module Timer.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

let root (model: Model) dispatch =
  div
    []
    [
      div
        []
        [input 
            [ClassName "input"
             Placeholder "html://ya.ru"
             Value !^ model.Url
             OnChange (fun e -> dispatch (UrlChanged (!!e.target?value)))
             ]
        ]
      br []
      div
        []
        [ span
            []
            [ str (model.Time.ToString "dd.MM.yyyy HH:mm:ss") ]
          span [] [ str " " ]
          (match model.Content with
           | None -> span [] [ str "---" ]
           | Some (Content.Body body) -> span [] [ str body ]
           | Some (Content.Error error) ->
              span 
                [Style 
                    [CSSProp.Color "red"
                     CSSProp.FontWeight !^ "bold" ]]
                [str error])
        ]
    ]