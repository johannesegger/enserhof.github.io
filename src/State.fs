module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map UeberDenHof (s "ueber-den-hof")
    map Aktivitaeten (s "aktivitaeten")
    map Lageplan (s "lageplan")
  ]

let urlUpdate (page: Option<Page>) model =
  let (model', cmd') =
    page
    |> FSharp.Core.Option.map (fun page ->
      { model with CurrentPage = page }, Cmd.none
    )
    |> FSharp.Core.Option.defaultWith (fun () ->
      console.error("Error parsing url")
      model, Navigation.modifyUrl (toHash model.CurrentPage)
    )
  
  Fable.Import.Browser.window.document.title <- toString model'.CurrentPage |> sprintf "%s | Enserhof z'Ehrndorf"

  GTag.setPage GTag.trackingId (toUrl model'.CurrentPage)

  model', cmd'

let init result =
  let ueberDenHof, ueberDenHofCmd = UeberDenHof.State.init
  let model = {
    CurrentPage = Aktivitaeten
    UeberDenHof = ueberDenHof
  }
  let model', cmd' = urlUpdate result model
  model', Cmd.batch [ cmd'; Cmd.map UeberDenHofMsg ueberDenHofCmd ]

let update msg model =
  match msg with
  | UeberDenHofMsg msg' ->
    let subModel, subCmd = UeberDenHof.State.update msg' model.UeberDenHof
    { model with UeberDenHof = subModel }, Cmd.map UeberDenHofMsg subCmd
