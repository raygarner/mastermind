'classic mastermind game in vb.net console mode

Module Module1
    Dim randoms(3) As Integer 'used to store the random numbers which will be transformed into colours
    Dim colours(3) As Char 'these are the colours that the user is attempting to discover
    Dim attemptletters(4) As Char 'this stores the user's attempt
    Dim validletters(8) As Char 'this stores the initials of all of the valid colours
    Dim userinputs(9) As String 'this stores each of the user's inputs for each turn
    Dim woutputs(9) As String 'this stores each of the white pin's outputs for each turn
    Dim routputs(9) As String 'this stores each of the red pin's outputs for each turn

    Sub Main()
        validletters(1) = "r"
        validletters(2) = "b"
        validletters(3) = "w"
        validletters(4) = "y"
        validletters(5) = "g"
        validletters(6) = "p"
        validletters(7) = "t"
        validletters(8) = "m"

        For r = 0 To 9
            userinputs(r) = "----"
            woutputs(r) = "-"
            routputs(r) = "-"
        Next

        Dim validinput As Boolean = False 'flag varaible to loop the request of whether the user wants duplicates or not
        Dim userinput1 As String = "" 'takes the user's firsts input askin whether they want duplicates in the objective
        Dim duplicates As Boolean 'remembers whether duplicates should be used in play or not

        Dim randomnumber As Integer 'a temp variable used during the generation of the four random numbers

        Dim colour As String 'four characters, each representing a colour, for the objective

        Dim userattempt As String 'stores the user's four letter attempt at guessing the objective

        Dim turns As Integer = 0

        Dim finish As Boolean = False

        takefirstinput(validinput, userinput1, duplicates) 'takes the user input

        Console.WriteLine("Generating objective...")
        Console.WriteLine("")

        generateobjective(duplicates, randomnumber, colour) 'generates the objective for the user to work towards

        Console.WriteLine(colour) 'for development

        Do Until turns = 9 'give the user 9 attempts

            If finish = True Then 'if the user completes the game
                turns = 9 'end the loop
            Else
                takeuserinput(userattempt, turns) 'stored in userattmempt and attemptletters(1-4)
                compareattempt(finish, turns, duplicates) 'compares the user's attempt with the objective and provides feedback

                turns = turns + 1 'increment turns



              



                For h = 8 To 0 Step -1 'outputs the table containing attempts and feedback for each attempt
                    Console.Write(userinputs(h))
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.Write(" " & routputs(h))
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(" " & woutputs(h))
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.WriteLine("")
                Next

                Console.WriteLine("")
                Console.WriteLine("You have " & (9 - turns) & " turns remaining.")
            End If
        Loop

        If finish = True Then 'if the loop ended bc the user guessed correctly 
            Console.Clear()
            Console.WriteLine("You win!")
            Console.ReadLine()
        Else
            Console.Clear()
            Console.WriteLine("You lose.")
            Console.ReadLine()
        End If
    End Sub

    Sub takefirstinput(ByRef validinput, ByRef userinput1, ByRef duplicates)
        Do While validinput = False
            Console.WriteLine("Do you want duplicates in the objective? Enter y or n.") 'asks the user whether they want the final objective to potentailly contain duplicate colours
            userinput1 = Console.ReadLine() ' takes the user's input
            Console.WriteLine("")
            userinput1 = LCase(userinput1) 'converts the user's input to lower case
            If userinput1 = "y" Then 'if they entered "y"
                duplicates = True 'allows potentail duplicates
                validinput = True 'ends the loop
            ElseIf userinput1 = "n" Then 'if they entered "n"
                duplicates = False 'disallows potential duplicates
                validinput = True 'ends the loop
            Else 'if the user entered anything other than "y" or "n"
                validinput = False 'the loop continues
                Console.WriteLine("Your request was invalid. Answer with a y or n.") 'tells the user that what they entered was invalid
            End If
        Loop
    End Sub 'working

    Sub generateobjective(ByRef duplicates, ByRef randomnumber, ByRef colour)
        If duplicates = False Then 'if the user wanted to have the possiblility of duplicates in their objective in play
            generaterandomwithnoduplicats(randomnumber) 'generates four random numbers without duplicates
        Else
            For u = 0 To 3
                Randomize() 'comfirms that the generated number is random
                randoms(u) = CInt(Rnd() * 7) 'generates a random number from 0 to 7
            Next
        End If
        generatecolours(colour) 'generates the colours in the objective based off the numbers previously generated
    End Sub 'working

    Sub generaterandomwithnoduplicats(ByRef random) 'working
        Dim validnumber As Boolean = False 'makes sure the function loops until the number is valid
        Dim check As Integer = 0 'increments for each fault in the number, used to confirm validity

        For n = 0 To 3 'generate 4 random numbers

            validnumber = False

            Do While validnumber = False 'keep generating until a valid one is generated
                check = 0 'resets the check
                Randomize() 'to confirm that the number will be random
                random = CInt(Rnd() * 7) 'generate a random number between 0 and 7

                For i = 0 To 3 'compare against four numbers
                    If random = randoms(i) Then 'compares the number to previously generating ones to check that they aren't the same
                        check = check + 1 'increment check and confirm that the number is invalid
                    End If
                Next

                If check <> 0 Then 'if check doesn't = 0 (if invalidities have been discovered)
                    validnumber = False 'forces the loop to continue
                Else
                    validnumber = True 'ends the loop
                    randoms(n) = random 'saves the current number into an array of four random numbers to be converted to colours
                End If
            Loop
        Next

    End Sub 'working

    Sub generatecolours(ByRef colour) 'working
        For c = 0 To 3 'generate four colours
            Select Case randoms(c) 'based on the random numbers that were generated
                Case 0
                    colours(c) = "r" 'red
                Case 1
                    colours(c) = "b" 'blue
                Case 2
                    colours(c) = "w" 'white
                Case 3
                    colours(c) = "y" 'yellow
                Case 4
                    colours(c) = "g" 'green
                Case 5
                    colours(c) = "p" 'pink
                Case 6
                    colours(c) = "t" 'teal
                Case 7
                    colours(c) = "m" 'maroon
            End Select

            colour = colour & colours(c) 'adds each letter to the string

        Next
    End Sub 'working

    Sub takeuserinput(ByRef userattempt, ByRef turns)
        Dim validinput As Boolean = False 'used as a flag representing whether or not what the user has entered is valid
        Dim check As Integer = 0 'used to check the validity of the user's request

        Do While validinput = False 'loop until the user enters something valid
            check = 0 'resets the value of the check variable each loop
            Console.WriteLine("")
            Console.WriteLine("Enter an attempt. Use four letters to represent each colour in your attempt") 'instructs the user
            Console.WriteLine("")
            Console.WriteLine("r = red")
            Console.WriteLine("b = blue")
            Console.WriteLine("w = white")
            Console.WriteLine("y = yellow")
            Console.WriteLine("g = green")
            Console.WriteLine("p = pink")
            Console.WriteLine("t = teal")
            Console.WriteLine("m = maroon")
            Console.WriteLine("")
            Console.Write("Attempt:")

            userattempt = Console.ReadLine() 'takes the user's input

            If Len(userattempt) <> 4 Then 'if the user enters anything with a length different to four characters
                check = check + 1 'increments the check variable
            End If

            userattempt = LCase(userattempt) 'converts the user's attempt all to lower case

            For n = 1 To 4 'loop 4 times
                attemptletters(n) = Mid(userattempt, n, 1) 'stores each letter of the userattempt string into a char array
            Next

            For j = 1 To 4 'loop for each letter in the user's attempt
                If attemptletters(j) <> "r" And attemptletters(j) <> "b" And attemptletters(j) <> "w" And attemptletters(j) <> "y" And attemptletters(j) <> "g" And attemptletters(j) <> "p" And attemptletters(j) <> "t" And attemptletters(j) <> "m" Then
                    'if anyinvalid letters are entered
                    check = check + 1 'make invalid

                End If
            Next

            If check <> 0 Then 'if it was flagged as invalid at any point
                validinput = False 'keep looping
                Console.WriteLine("Invalid attempt") 'tell the user that their attempt was invalid
                Console.WriteLine("")

                For h = 8 To 0 Step -1 'outputs the table containing the attempts and feedback from each attempt
                    Console.Write(userinputs(h))
                    Console.ForegroundColor = ConsoleColor.Red
                    Console.Write(" " & routputs(h))
                    Console.ForegroundColor = ConsoleColor.White
                    Console.Write(" " & woutputs(h))
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.WriteLine("")
                Next
               
            Else
                validinput = True 'stops the loop
                Console.WriteLine("Processing attempt") 'tell the user that their attempt was valid. Useful for debugging
                Console.WriteLine("")
                userinputs(turns) = userattempt 'saves the current user input
            End If

            'Console.WriteLine("input check loop")
        Loop
    End Sub 'working

    Sub compareattempt(ByRef finish, ByRef turns, ByVal duplicates) 'not working
        'Console.WriteLine("compareattempt")
        Dim correct As Integer = 0 'represents the red pins
        Dim halfright As Integer = 0 'represents the white pins
        Dim correctletters(4) 'stores the letters that have been found to match
        Dim halfrights(4) 'stores the values of the halfrights
        Dim colourscheck(3) As String
        Dim attemptletterscheck(4) As String

        For t = 1 To 4
            correctletters(t) = "o" 'resets the elements of the array
        Next

        correct = 0
        halfright = 0


        For k = 0 To 3 'copies the arrays so that these ones can be edited
            colourscheck(k) = colours(k)
            attemptletterscheck(k + 1) = attemptletters(k + 1)
        Next

        'checks for red pins
        For h = 1 To 4 'loop for each letter in the attempt
            If attemptletterscheck(h) = colourscheck(h - 1) Then 'if there is a match
                correct = correct + 1
                attemptletterscheck(h) = "_" 'essentially deletes that element from the array
                colourscheck(h - 1) = "-" 'essentially deletes that element from the array

            End If
        Next

        'checks for white pins
        For n = 1 To 4
            For a = 0 To 3
                If attemptletterscheck(n) = colourscheck(a) Then
                    halfright = halfright + 1
                    colourscheck(a) = "-"
                    attemptletterscheck(halfright) = "_"
                    a = 2
                End If
            Next
        Next

        'halfright = halfright - correct 'this stops correct letters being represented as halfright aswell

        If correct = 4 Then 'if all four pins are corrects
            finish = True 'the game ends
            Console.WriteLine("finish = true")
        

        End If

        woutputs(turns) = halfright 'saves the current halfrights 
        routputs(turns) = correct 'saves the current corrects

        halfright = 0
    End Sub 'working

End Module
