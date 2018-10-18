module BasicTypes {

    let number: number = 123;
    let string: string = "DEJA VU, I'VE BEEN IN THIS PLACE BEFORE";
    let owntype: "Movie" = "Movie";
    let uniontypes: number | string = 12345;
    let uniontypesagain: "Loading" | number | "Error" = "Loading";
    enum Color { Red, Blue };
    let colors: Color = Color.Red;

    // export let main: Function = () => console.log(string); // If you want to use a variable outside of the module, you need to place "export" in front of it.
}

// BasicTypes.main(); // Using the function that we have "exported" outside of the BasicTypes module.

module UnionTypes {

    type Movie = { Kind: "Movie", Title: string, ReleaseYear: string, Genre: string } // When we are using our model later on, we should match the attributes of the table with the TS code so that the JSON can process it without errors.
    type Actor = { Kind: "Actor", Name: string, BirthDate: string, Role: string } // The "Kind" keyword lets us extinguish between the types, which is called "Discriminated Union".
    export type Record = Movie | Actor;

    let m: Movie = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" }
    let a: Actor = { Kind: "Actor", Name: "Braddy Pitter", BirthDate: "25-05-1980", Role: "Lead" }
    let r: Record = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" }
    export let main: Function = (r: Record) => {
        switch (r.Kind) {
            case "Movie":
                // The line below processes the DOM to HTML while using the Id to see what to couple the information it has with on the HTML side of things.
                document.getElementById("Practice").innerHTML = r.Title + " | " + r.ReleaseYear + " | " + r.Genre
                document.getElementById("Practice").style.backgroundColor = "Blue"
                // console.log(r.Title + " | " + r.ReleaseYear + " | " + r.Genre) // Printing stuff in the console.
                break;

            case "Actor":
                document.getElementById("Practice").innerHTML = r.Name + " | " + r.BirthDate + " | " + r.Role
                document.getElementById("Practice").style.backgroundColor = "Red"
                // console.log(r.Name + " | " + r.BirthDate + " | " + r.Role)
                break;

            default:
                break;
        }
    }
}

// let r1: UnionTypes.Record = {Kind: "Movie", Title: "Creed", ReleaseYear: "2015", Genre: "Action/Thriller"};
// let r2: UnionTypes.Record = {Kind: "Actor", Name: "Michael B. Jordan", BirthDate: "18-10-2018", Role: "Adonis Creed"};

// UnionTypes.main(r2);

module UnionTypesWithIntersection {

    type Movie = { Title: string, ReleaseYear: string, Genre: string } // When we are using our model later on, we should match the attributes of the table with the TS code so that the JSON can process it without errors.
    type Actor = { Name: string, BirthDate: string, Role: string } // The "Kind" keyword lets us extinguish between the types, which is called "Discriminated Union".
    export type Record = Movie & {Kind: "Movie"} | Actor & {Kind: "Actor"};

    let r: Record = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" }

    export let main: Function = (r: Record) => {
        switch (r.Kind) {
            case "Movie":
                // The line below processes the DOM to HTML while using the Id to see what to couple the information it has with on the HTML side of things.
                document.getElementById("Practice").innerHTML = r.Title + " | " + r.ReleaseYear + " | " + r.Genre
                document.getElementById("Practice").style.backgroundColor = "Blue"
                // console.log(r.Title + " | " + r.ReleaseYear + " | " + r.Genre) // Printing stuff in the console.
                break;

            case "Actor":
                document.getElementById("Practice").innerHTML = r.Name + " | " + r.BirthDate + " | " + r.Role
                document.getElementById("Practice").style.backgroundColor = "Red"
                // console.log(r.Name + " | " + r.BirthDate + " | " + r.Role)
                break;

            default:
                break;
        }
    }
}

// UnionTypesWithIntersection.main(r1);

// Currying is the technique of translating the evaluation of a function that takes multiple arguments into evaluating a sequence of functions.

module Currying {

    type Fun<A, B> = (_: A) => B; // For any A, it will produce a B.
    let pipeline : <A, B, C>(_:Fun<A,B>) => (_:Fun<B,C>) => Fun<A,C> = f => g => (x => g(f(x))); // f = Fun<A,B>, g = Fun<B,C>

    let increment : (_:number) => number = x => x+1;
    let double : (_:number) => number = x => x*2;
    let half : (_:number) => number = x => x/2;

    let p1 = pipeline(increment)(half)
    let p2 = pipeline(double)(increment)
    let p3 = pipeline(double)(pipeline(increment)(half))
    

    export let main: Function = () => console.log(p1(2), p2(2), p3(2)); // x = 2 in this case.
}

Currying.main();