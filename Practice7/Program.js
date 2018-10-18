var BasicTypes;
(function (BasicTypes) {
    var number = 123;
    var string = "DEJA VU, I'VE BEEN IN THIS PLACE BEFORE";
    var owntype = "Movie";
    var uniontypes = 12345;
    var uniontypesagain = "Loading";
    var Color;
    (function (Color) {
        Color[Color["Red"] = 0] = "Red";
        Color[Color["Blue"] = 1] = "Blue";
    })(Color || (Color = {}));
    ;
    var colors = Color.Red;
    // export let main: Function = () => console.log(string); // If you want to use a variable outside of the module, you need to place "export" in front of it.
})(BasicTypes || (BasicTypes = {}));
// BasicTypes.main(); // Using the function that we have "exported" outside of the BasicTypes module.
var UnionTypes;
(function (UnionTypes) {
    var m = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" };
    var a = { Kind: "Actor", Name: "Braddy Pitter", BirthDate: "25-05-1980", Role: "Lead" };
    var r = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" };
    UnionTypes.main = function (r) {
        switch (r.Kind) {
            case "Movie":
                // The line below processes the DOM to HTML while using the Id to see what to couple the information it has with on the HTML side of things.
                document.getElementById("Practice").innerHTML = r.Title + " | " + r.ReleaseYear + " | " + r.Genre;
                document.getElementById("Practice").style.backgroundColor = "Blue";
                // console.log(r.Title + " | " + r.ReleaseYear + " | " + r.Genre) // Printing stuff in the console.
                break;
            case "Actor":
                document.getElementById("Practice").innerHTML = r.Name + " | " + r.BirthDate + " | " + r.Role;
                document.getElementById("Practice").style.backgroundColor = "Red";
                // console.log(r.Name + " | " + r.BirthDate + " | " + r.Role)
                break;
            default:
                break;
        }
    };
})(UnionTypes || (UnionTypes = {}));
// let r1: UnionTypes.Record = {Kind: "Movie", Title: "Creed", ReleaseYear: "2015", Genre: "Action/Thriller"};
// let r2: UnionTypes.Record = {Kind: "Actor", Name: "Michael B. Jordan", BirthDate: "18-10-2018", Role: "Adonis Creed"};
// UnionTypes.main(r2);
var UnionTypesWithIntersection;
(function (UnionTypesWithIntersection) {
    var r = { Kind: "Movie", Title: "Fight Club", ReleaseYear: "25-05-1990", Genre: "Action/Thriller" };
    UnionTypesWithIntersection.main = function (r) {
        switch (r.Kind) {
            case "Movie":
                // The line below processes the DOM to HTML while using the Id to see what to couple the information it has with on the HTML side of things.
                document.getElementById("Practice").innerHTML = r.Title + " | " + r.ReleaseYear + " | " + r.Genre;
                document.getElementById("Practice").style.backgroundColor = "Blue";
                // console.log(r.Title + " | " + r.ReleaseYear + " | " + r.Genre) // Printing stuff in the console.
                break;
            case "Actor":
                document.getElementById("Practice").innerHTML = r.Name + " | " + r.BirthDate + " | " + r.Role;
                document.getElementById("Practice").style.backgroundColor = "Red";
                // console.log(r.Name + " | " + r.BirthDate + " | " + r.Role)
                break;
            default:
                break;
        }
    };
})(UnionTypesWithIntersection || (UnionTypesWithIntersection = {}));
// UnionTypesWithIntersection.main(r1);
// Currying is the technique of translating the evaluation of a function that takes multiple arguments into evaluating a sequence of functions.
var Currying;
(function (Currying) {
    var pipeline = function (f) { return function (g) { return (function (x) { return g(f(x)); }); }; }; // f = Fun<A,B>, g = Fun<B,C>
    var increment = function (x) { return x + 1; };
    var double = function (x) { return x * 2; };
    var half = function (x) { return x / 2; };
    var p1 = pipeline(increment)(half);
    var p2 = pipeline(double)(increment);
    var p3 = pipeline(double)(pipeline(increment)(half));
    Currying.main = function () { return console.log(p1(2), p2(2), p3(2)); }; // x = 2 in this case.
})(Currying || (Currying = {}));
Currying.main();
