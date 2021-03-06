module Administration.State

open System
open Elmish
open Elmish.Toastr
open Types
open Types.GitHubApi
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser
open Fable.Helpers.Moment
open Fable.PowerPack
open Fable.PowerPack.Fetch

let private tryGetGitHubAccessToken () =
  Fable.Import.Browser.localStorage.getItem "GITHUB_ACCESS_TOKEN"
  :?> string
  |> FSharp.Core.Option.ofObj

let private setGitHubAccessToken value =
  Fable.Import.Browser.localStorage.setItem("GITHUB_ACCESS_TOKEN", value)

let [<PassGenerics>] private putRecordAndParseResponse url record properties= promise {
  let! response = Fable.PowerPack.Fetch.putRecord url record properties
  let! text = response.text()
  return ofJson text
}

let rec update msg model =
  match msg with
  | LoadStallzeiten ->
    let cmd =
      Cmd.ofPromise
        (fetchAs<GitHubApi.GetContentResponse> "https://api.github.com/repos/enserhof/enserhof.github.io/contents/public/api/stallzeiten")
        [ requestHeaders [ Authorization (sprintf "Bearer %s" model.GitHubAccessToken) ] ]
        LoadStallzeitenSuccess
        (LoadStallzeitenError.HttpError >> LoadStallzeitenError)
    { model with RemoteStallzeiten = Loading }, cmd
  | LoadStallzeitenSuccess file ->
    try
      let stallzeiten =
        file.content
        |> window.atob
        |> ofJson<DateTime list>
      let model' =
        { model with
            RemoteStallzeiten = Loaded { Version = file.sha; FileUrl = file.url; Stallzeiten = stallzeiten }
            LocalStallzeiten =
              stallzeiten
              |> List.map (fun timestamp ->
                { Id = System.Guid.NewGuid().ToString() ; Value = Valid timestamp })
        }
      model', []
    with e ->
      update (LoadStallzeitenError (ParseError e)) model
  | LoadStallzeitenError ((LoadStallzeitenError.HttpError e) as error) ->
    Fable.Import.Browser.console.error("Error while loading Stallzeiten: ", e)
    { model with RemoteStallzeiten = LoadError error }, []
  | LoadStallzeitenError ((ParseError e) as error) ->
    Fable.Import.Browser.console.error("Error while parsing Stallzeiten: ", e)
    { model with RemoteStallzeiten = LoadError error }, []
  | UpdateGitHubAccessToken value ->
    { model with GitHubAccessToken = value }, []
  | Login ->
    setGitHubAccessToken model.GitHubAccessToken
    update LoadStallzeiten model
  | UpdateStallzeit (timeId, time) ->
    let updateTime stallzeit =
      let value =
        match moment.Invoke(time, U3.Case1 "YYYY-MM-DDTHH:mm", true) with
        | value when value.isValid() -> Valid(value.toDate())
        | _ -> Invalid time
      if timeId = stallzeit.Id
      then { Id = timeId; Value = value }
      else stallzeit
    { model with LocalStallzeiten = model.LocalStallzeiten |> List.map updateTime }, []
  | AddStallzeit ->
    let newStallzeit = {
      Id = Guid.NewGuid().ToString()
      Value = Valid (DateTime.Today.AddDays(1.).AddHours(8.5)) }
    { model with LocalStallzeiten = model.LocalStallzeiten @ [ newStallzeit ] }, []
  | RemoveStallzeit timeId ->
    let stallzeiten' =
      model.LocalStallzeiten
      |> List.filter (fun t -> t.Id <> timeId)
    { model with LocalStallzeiten = stallzeiten' }, []
  | SaveStallzeiten ->
    match model.RemoteStallzeiten with
    | Loaded remoteStallzeiten ->
      let stallzeiten' =
        model.LocalStallzeiten
        |> List.choose (fun item ->
          match item.Value with
          | Valid value -> Some value
          | Invalid _ -> None
        )
      let body = {
        message = "Update Stallzeiten"
        content = toJson stallzeiten' |> window.btoa
        sha = remoteStallzeiten.Version
        branch = "dev"
      }
      let cmd =
        Cmd.ofPromise
          (putRecordAndParseResponse remoteStallzeiten.FileUrl body)
          [ requestHeaders [ Authorization (sprintf "Bearer %s" model.GitHubAccessToken) ] ]
          (fun response -> SaveStallzeitenSuccess response.content.sha)
          (SaveStallzeitenError.HttpError >> SaveStallzeitenError)
      model, cmd
    | _ -> model, []
  | SaveStallzeitenSuccess version ->
    printfn "Success"
    match model.RemoteStallzeiten with
    | Loaded stallzeiten ->
      let model' =
        { model with
            RemoteStallzeiten =
              Loaded
                { stallzeiten with
                    Version = version
                }
        }
      let cmd =
        Toastr.message "Stallzeiten wurden erfolgreich gespeichert"
        |> Toastr.title "Stallzeiten speichern"
        |> Toastr.showCloseButton
        |> Toastr.success
      model', cmd
    | _ -> model, []
  | SaveStallzeitenError (SaveStallzeitenError.HttpError e) ->
    Fable.Import.Browser.console.error("Error while saving Stallzeiten: ", e)
    let cmd =
      Toastr.message "Fehler beim Speichern der Stallzeiten"
      |> Toastr.title "Stallzeiten speichern"
      |> Toastr.showCloseButton
      |> Toastr.error
    model, cmd

let init =
  let gitHubAccessToken = tryGetGitHubAccessToken()
  let model = {
    GitHubAccessToken = gitHubAccessToken |> FSharp.Core.Option.defaultValue ""
    RemoteStallzeiten = NotLoaded
    LocalStallzeiten = [] }
  match gitHubAccessToken with
  | Some _ -> update LoadStallzeiten model
  | None -> model, Cmd.none
