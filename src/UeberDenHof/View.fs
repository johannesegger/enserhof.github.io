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
    Content.content []
      [ str "Der Enserhof ist seit vier Generationen ein Familienbetrieb."
        br []
        str "Früher wurde er vor allem zu Selbstversorgungszwecken genützt."
        br []
        str "Bis 1995 wurden hauptsächlich Kühe zur Milchproduktion gehalten."
        br []
        str "Zwischen 1995 und 2014 war der Betrieb dann eine reine Schweinezucht und -mast."
        br []
        str "Wir (Sylvia und Johannes) sind seit 2011 auf dem Hof."
        br []
        str "2012 sind die beiden Esel Lilly und Paula als Ausgleich vom Arbeitsalltag zu uns gekommen."
        br []
        str "2016 ist der Vierkanthof durch die Entfernung von zwei Seiten geöffnet worden und bietet seit dem Platz für unseren Garten." ]
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
