module Global

type Page =
  | Home
  | Counter
  | Timer
  | About

let toHash page =
  match page with
  | About -> "#about"
  | Counter -> "#counter"
  | Timer -> "#timer"
  | Home -> "#home"
