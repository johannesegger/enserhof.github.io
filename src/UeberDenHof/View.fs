module UeberDenHof.View

open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

let root =
  [ Heading.h1 [ Heading.Is3 ]
      [ str "Über den Hof" ]
    Heading.h2 [ Heading.Is4 ]
      [ str "Tiere" ]
    Tile.ancestor [ ]
      [ Tile.parent [ Tile.IsVertical; Tile.Size Tile.Is8 ]
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Lilly & Paula" ]
                Image.image [ Image.Is3by2 ] [ img [ Src (importAll "../../images/tiere/esel.jpg") ] ] ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Laika & Luna" ] ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Josephine & Rosi" ] ] ]
        Tile.parent [ Tile.IsVertical ]
          [ Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Minki, Quetschi und Familie" ]
                Image.image [ Image.Is5by3 ] [ img [ Src (importAll "../../images/tiere/hasen.jpg") ] ] ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Hahn & Hühner" ] ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Puma" ]
                Image.image [ Image.Is3by2 ] [ img [ Src (importAll "../../images/tiere/hund.jpg") ] ] ]
            Tile.child [ Tile.CustomClass Box.Classes.Container ]
              [ Heading.h3 [ Heading.Is5; Heading.CustomClass "no-bullet" ] [ str "Maxi" ] ] ] ] ]
