
# Two-Player Board Game Framework

This framework provides an extensible solution for many different two-player board games. It includes two games:

## Class Diagram
The final class diagram has expanded functions and modified relationships while staying consistent with the original design.
<img width="569" alt="image" src="https://github.com/SeanBaek111/TicTacToe/assets/33170173/2cdae57c-56c7-43a0-ba96-1568b8b93e2a">


## Numerical Tic-Tac-Toe

A variation of the classic Tic-Tac-Toe game. Two players take turns placing the numbers 1 to 9 on a 3x3 board. The first player plays with the odd numbers, the second player plays with the even numbers. The player who first puts down 15 points in a line (sum of a horizontal, vertical, or diagonal row of three numbers) wins the game.
 
#### Member Variables

-   `players`: player array
-   `currentPlayer`: current player
-   `winPlayer`: winning player
-   `currentBoardStatus`: current board status
-   `gameBoard`: game board
-   `nCurrentTurn`: current turn number

#### Methods

-   `Play()`: runs the game
-   `IsWin()`: checks if there is a winning player on the current board
-   `IsQuit()`: checks if a player has quit the game
-   `SetBoard()`: sets the game board
-   `SetPlayers()`: sets the player array
 
## Wild Tic-Tac-Toe

Like the classic Tic-Tac-Toe game, two players take turns placing an X or an O piece on a 3x3 board. However, in this game players can choose to place either X or O on each move. The first player who creates a line of three X's in a row or three O's in a row (horizontally, vertically, or diagonally) wins the game.
 

#### Member Variables

-   `players`: player array
-   `currentPlayer`: current player
-   `winPlayer`: winning player
-   `currentBoardStatus`: current board status
-   `gameBoard`: game board
-   `nCurrentTurn`: current turn number

#### Methods

-   `Play()`: runs the game
-   `IsWin()`: checks if there is a winning player on the current board
-   `IsQuit()`: checks if a player has quit the game
-   `SetBoard()`: sets the game board
-   `SetPlayers()`: sets the player array
 
