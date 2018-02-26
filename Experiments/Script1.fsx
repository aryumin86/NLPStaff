let rec fact = function
    1 -> 1
    | x -> x * fact(x-1)

let x = 5
printf "factorial of %i is %i" x (fact x)
