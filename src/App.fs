module App

open Sutil
open Sutil.CoreElements

type Model = { count: int }

type Msg =
    | Increment
    | Decrement

let init () : Model * Cmd<Msg> =
    let model = { count = 0 }
    let cmd = Cmd.none
    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Increment -> { model with count = model.count + 1 }, Cmd.none
    | Decrement -> { model with count = model.count - 1 }, Cmd.none

let getCounter m = m.count

let app () =
    let model, dispatch = () |> Store.makeElmish init update ignore

    Html.div [ 
        Html.h1 [
            text "Hello world!"
        ]
        Html.div [
            Html.p [
                Bind.fragment (model |> Store.map getCounter) (fun n -> text $"Counter = {n}")
            ]
        ]
        Html.div [
            Html.button [
                onClick (fun _ -> dispatch Increment) []
                text "(+)"
            ]
        ]
        Html.div [
            Html.button [
                onClick (fun _ -> dispatch Decrement) []
                text "(-)"
            ]
        ]
    ]

app () |> Program.mount |> ignore
