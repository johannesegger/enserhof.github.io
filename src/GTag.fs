module GTag

open Fable.Core

let trackingId = "UA-123409256-1"

[<Emit("gtag('config', '$1', {'page_path': '$2'});")>]
let setPage (gaTrackingId: string) (pagePath: string): unit = jsNative
