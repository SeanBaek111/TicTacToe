using System;
namespace TicTacToe
{
    public class Game
    {
        Player[] players;        
        Player currentPlayer;
        string currentBoardStatus;
        Board gameBoard;

        private int nCurrentTurn;
        const int MAX_TURN = 9;
        public Game()
        {
            nCurrentTurn = 0; 
        }

        private Player GetCurrentPlayer()
        {
            Player player = null;

            foreach(Player p in players)
            {
                if( p.IsTurn() )
                {
                    player = p;
                    break;
                }
            }
            Console.WriteLine("Current Player : " + player.GetName());
            return player;
        }
        public void Play()
        {            
            Console.WriteLine("Game started");
            gameBoard.DisplayBoard();
            while (!IsQuit() || IsWin())
            {

                currentPlayer = GetCurrentPlayer();
              
                currentPlayer.MakeMovement(gameBoard);
                gameBoard.DisplayBoard();
                SwapPlayer(); 

                BoardStatus boardStatus = new BoardStatus(currentPlayer, currentBoardStatus);
                nCurrentTurn = AddHistory(boardStatus);                
            }
            

            Console.WriteLine("Game Finished");
        }

        private bool IsWin()
        {
            return false;
        }
        private bool IsQuit()
        {
            if (nCurrentTurn >= MAX_TURN)
                return true;
            return false;
        }

        private void SwapPlayer()
        {            
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].IsTurn())
                {
                    players[i].SetTurn(false);
                    if( i == players.Length -1)
                    {
                        players[0].SetTurn(true);
                    }
                    else
                    {
                        players[i+1].SetTurn(true);
                    }
                    break;
                }
                
            }

        }

        public void SetBoard(Board board)
        {
            this.gameBoard= board;
        }
        internal void SetPlayers(Player[] players)
        {
            this.players = players;
        }

        private int AddHistory(BoardStatus boardStatus)
        {
            int nHistoryCnt =    History.GetInstance().Push(boardStatus);
            Console.WriteLine("nHistoryCnt " + nHistoryCnt);

            return nHistoryCnt;

        }

    }
}

