open System
open System.Linq
open System.Text.RegularExpressions
open System.IO
open System.Text
open System.Collections.Generic


(*
Gets corpus of texts, tokenizes texts, calculates the probabilities of n-gramms (N could
be any integer and generates some speech using n-gramms)
*)


//Dictionary of n-gramms. Key is a n-gramm as a string and value is its count in corpus.
let rawNGrammsDict = Dictionary<string, int>()


//Clean text
let cleanText (raw : string) =
    let signsToReplace = [|',';'@';'#';'$';'%';'^';'&';'*';'(';')';'+';'=';'\'';'\"';':';';';'<';'>';'№';'\t';'\n'|]

    let repl (ch: char) =
        if signsToReplace.Contains(ch) then ' ' else ch

    let cleaned =
        raw.ToLower().ToCharArray()
        |> Array.map (fun x -> repl x)
        
    let joined = String.Join("", cleaned)

    Regex.Replace(joined, @"\s+", " ")


//Tokenize text
let tokenizeToWords (raw : string) =
    raw.Split(' ')


//Tokenize string to sentences (saving  '?', '.', '?')
let tokenizeToSentenses (corpus : string) = 
    corpus.Split([|'!';'?';'.'|])


//Is a string at the end of sentence
//let isItASentenceEnd (str : string) = 
//    if str.Length = 0 then 
//        false
//    else
//        let sentenceEnds = [|'.';'?';'!'|]
//        Array.exists (fun x -> x = str.ToCharArray().[str.Length-1]) sentenceEnds


//N-gramm type
type NGramm (rawStr : string) =
    member val AtSentenceEnd = false with get, set
    member val Raw = rawStr with get
    member val Tokens = rawStr |> cleanText |> tokenizeToWords with get


//Dictionary of beginnings of n-gramms - key is a 1st word in a n-gramm
let nGramms1stWordsDict = Dictionary<string, NGramm>()


//Dictionary of probabilities of n-gramms
//If there is already such a propbability (key) we add 0.000001 to it and chrck for this key again
//Adding to this dict should been done by func addToNGrammsProbabilitiesDict
let nGrammsProbabilitiesDict = Dictionary<double, NGramm>()


//Adding probabilities of n-gramms to dictionary
//let rec addToNGrammsProbabilitiesDict key ng =
//    if nGrammsProbabilitiesDict.ContainsKey(key) then 
//        addToNGrammsProbabilitiesDict (key + 0.0001) ng
//    else 
//        nGrammsProbabilitiesDict.Add(key, ng)


//Get all names of files in directory
let getFilesNamesInDir path = 
    Directory.GetFiles(path)


//Load text from local file
let loadTextFromFile path enc = 
    File.ReadAllText(path, enc)


//Get n-gramms for array of tokens (preferably of 1 sentence)
let rec getNGramms (raw : string[], n : int, startIndex : int, endIndex : int) = 
    if endIndex < raw.Length then
        let ng = String.Join(" ", Array.sub raw startIndex n)

        if rawNGrammsDict.ContainsKey(ng) then 
            let oldNgFreq = rawNGrammsDict.[ng]
            rawNGrammsDict.[ng] <- oldNgFreq + 1
        else
            rawNGrammsDict.Add(ng, 1)

        getNGramms(raw, n, startIndex + n, endIndex + n)
    


//Compute probabilities for n-gramms for nGrammsProbabilitiesDict
let computeNGrammsProbabilities = 
    let totalNGrammsCount = rawNGrammsDict.Values.Sum()
    for x in rawNGrammsDict do 
        let ng = new NGramm(x.Key)
        nGrammsProbabilitiesDict.Add((double)x.Value / (double)totalNGrammsCount, ng)


//Create a dictionary of n-gramms 1-st words
let computeNGramms1stWordsDictionary = 
    ""


//Get n-gramm with certain probability



//Get n-gramm starting with certain word



//Generate some bullshit... (speech)



//Get most probable n-grams


//Main corpus loading, n-gramms creating and computation function
let doStaff(dir : string, enc : Encoding, numOfSentencesToGen : int, n: int) = 
    //loading texts
    let files = getFilesNamesInDir dir
    let mutable corpus = ""
    for f in files do 
        corpus <- String.concat " " [|corpus; (loadTextFromFile f enc)|]

    //tokenizing corpus to centences
    let sentencesOfCorpus = tokenizeToSentenses(corpus)
    
    //creating dictionary of ngramms (as text) frequencies
    for s in sentencesOfCorpus do
        let ss = s |> cleanText |> tokenizeToWords
        getNGramms(ss, n, 0, n)
        
    printf "number of keys in n-gramm dict is %i " rawNGrammsDict.Keys.Count

    //creating ngramms-1st-word dictionary


    //creating probabilities of n-gramms dictionary


    //starting main loop of sentecies generating
    let generateSentence = ""
    for i in 1 .. numOfSentencesToGen do
        printfn "not implemented main loop..."      
         
    


////////////////////////////////////////////////////////////////
////////////////////////Experiments/////////////////////////////
////////////////////////////////////////////////////////////////
let mutable ng = new NGramm("это какая-то n-грамма вот", 2)
printfn "end? %b " ng.AtSentenceEnd
printfn "raw: %s " ng.Raw 

doStaff(@"C:\Users\aryumin\Desktop\texts", Encoding.UTF8, 3, 3)