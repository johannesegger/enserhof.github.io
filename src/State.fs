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
  ]

let setPageTitle page =
  Fable.Import.Browser.window.document.title <- toString page |> sprintf "%s | Enserhof z'Ehrndorf"

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
    console.error("Error parsing url")
    setPageTitle model.CurrentPage
    model, Navigation.modifyUrl (toHash model.CurrentPage)
  | Some page ->
      setPageTitle page
      { model with CurrentPage = page }, []

let init result =
  urlUpdate result { CurrentPage = Aktivitaeten }

let update () model =
  model, Cmd.none
