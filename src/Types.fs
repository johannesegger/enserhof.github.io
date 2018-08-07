module App.Types

open Global

type Message =
  | UeberDenHofMsg of UeberDenHof.Types.Msg

type Model = {
  CurrentPage: Page
  UeberDenHof: UeberDenHof.Types.Model
}
