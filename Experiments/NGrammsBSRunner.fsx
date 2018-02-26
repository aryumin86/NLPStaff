#load "NGrammsBullshitGenerator.fsx"

open System
open NGrammsBullshitGenerator
open System.Text

let corpusDir = @"C:\Users\aryumin\Desktop\texts"

doStaff(corpusDir, Encoding.UTF8б 3)
