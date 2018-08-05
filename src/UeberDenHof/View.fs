module UeberDenHof.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

let root =
  let h3 text =
    Heading.h3 [ Heading.Is5 ] [ str text ]
  let image src =
    Image.image [ ] [ img [ Src src ] ]

  [ Heading.h1 [ Heading.Is3 ]
      [ str "Über den Hof" ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Tiere" ]
    Tile.ancestor [ ]
      [ Tile.parent [ Tile.IsVertical; Tile.Size Tile.Is8 ]
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Lilly & Paula"
                image (importAll "../../images/tiere/esel.jpg") ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Laika & Luna"
                image (importAll "../../images/tiere/ponys.jpg") ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Josephine & Rosi"
                image (importAll "../../images/tiere/kuehe.jpg") ] ]
        Tile.parent [ Tile.IsVertical ]
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Minki, Quetschi und Familie"
                image (importAll "../../images/tiere/hasen.jpg") ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Hahn & Hühner"
                image (importAll "../../images/tiere/huehner.jpg") ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Puma"
                image (importAll "../../images/tiere/hund.jpg") ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ h3 "Maxi"
                image (importAll "../../images/tiere/maxi.jpg") ] ] ] ]
