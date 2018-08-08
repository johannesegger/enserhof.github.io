module App.View

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Core.JsInterop
open App.State
open App.Types
open Global
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Fulma

importAll "../node_modules/bulma/bulma.sass"
importAll "../sass/main.sass"

let menuItem page currentPage =
  Navbar.Item.a
    [ Navbar.Item.IsActive (page = currentPage)
      Navbar.Item.Props [ Href (toHash page) ] ]
    [ str (toString page) ]

let menu currentPage =
  Navbar.navbar [ Navbar.Color IsLight ]
    [ for page in allPages -> menuItem page currentPage ]

let root model dispatch =
  let pageHtml =
    function
    | Aktivitaeten -> Aktivitaeten.View.root model.Aktivitaeten
    | UeberDenHof -> UeberDenHof.View.root model.UeberDenHof (UeberDenHofMsg >> dispatch)
    | Lageplan -> Lageplan.View.root
  
  div []
    [ Hero.hero [ ]
        [ Hero.body [ ]
            [ Container.container [ Container.IsFluid ]
                [ Heading.h2
                    [ Heading.Is4
                      Heading.IsSubtitle
                      Heading.Modifiers [ Modifier.TextColor IsLight ] ]
                    [ str "Herzlich Willkommen am" ]
                  Heading.h1 [ Heading.Modifiers [ Modifier.TextColor IsWhite ] ]
                    [ str "Enserhof z'Ehrndorf" ] ] ] ]
      menu model.CurrentPage
      Section.section [ Section.CustomClass "main-section" ]
        [ Container.container [ ]
            (pageHtml model.CurrentPage) ]
    ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
|> Program.run
