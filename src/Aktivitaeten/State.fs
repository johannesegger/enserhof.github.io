module Aktivitaeten.State

open System
open Elmish
open Types
open Fable.PowerPack
open Fable.PowerPack.Fetch

let init : Model * Cmd<Msg> =
  let loadStallzeitenCmd =
    Cmd.ofPromise
      (fetchAs<DateTime list> "/api/stallzeiten")
      []
      LoadStallzeitenSuccess
      (HttpError >> LoadStallzeitenError)
  { Stallzeiten = Loading }, loadStallzeitenCmd

let update msg model =
  match msg with
  | LoadStallzeitenSuccess items ->
     { model with Stallzeiten = Loaded items }, []
  | LoadStallzeitenError error ->
     { model with Stallzeiten = LoadError error }, []
