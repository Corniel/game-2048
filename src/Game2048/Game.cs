using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Troschuetz.Random;

namespace Game2048
{
	public class Game
	{
		private List<Board> m_Moves = new List<Board>();

		public Board Current { get { return m_Moves.Last(); } }
		public int MoveCount { get { return m_Moves.Count; } }
		public bool IsActive { get { return Current.IsActive; } }

		public Board Move(Move move, IGenerator rnd)
		{
			var board = Current.Move(move);
			
			// No changes, invalid move.
			if (board == Current)
			{
				return board;
			}
			board = board.FillEmptySpot(rnd);

			m_Moves.Add(board);
			return board;
		}

		public string Formatted()
		{
			var sb = new StringBuilder();
			sb.AppendLine(Current.ToString());
			sb.AppendFormat("Moves: {0}", MoveCount);
			if (!IsActive) 
			{
				sb.Append('*');
			}
			return sb.ToString();
		}
		
		#region I/O operations

		/// <summary>Saves the game to a file.</summary>
		/// <param name="fileName">
		/// The name of the file.
		/// </param>
		/// <param name="mode">
		/// The file mode.
		/// </param>
		public void Save(string fileName, FileMode mode = FileMode.Create) { Save(new FileInfo(fileName), mode); }

		/// <summary>Saves the game to a file.</summary>
		/// <param name="file">
		/// The file to save to.
		/// </param>
		/// <param name="mode">
		/// The file mode.
		/// </param>
		public void Save(FileInfo file, FileMode mode = FileMode.Create)
		{
			using (var stream = new FileStream(file.FullName, mode, FileAccess.Write))
			{
				Save(stream);
			}
		}

		/// <summary>Saves the game to a stream.</summary>
		/// <param name="stream">
		/// The stream to save to.
		/// </param>
		public void Save(Stream stream)
		{
			var serializer = new BinaryFormatter(); 
			serializer.Serialize(stream, this);
		}

		/// <summary>Loads the game from a file.</summary>
		/// <param name="fileName">
		/// The name of the file.
		/// </param>
		public static Game Load(string fileName) { return Load(new FileInfo(fileName)); }

		/// <summary>Loads the game from a file.</summary>
		/// <param name="file">
		/// The file to load from.
		/// </param>
		public static Game Load(FileInfo file)
		{
			using (var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
			{
				return Load(stream);
			}
		}

		/// <summary>Loads the game from a stream.</summary>
		/// <param name="stream">
		/// The stream to load from.
		/// </param>
		public static Game Load(Stream stream)
		{
			var serializer = new BinaryFormatter();
			return (Game)serializer.Deserialize(stream);
		}

		#endregion

		public static Game Create(IGenerator rnd)
		{
			return Create(Board.Initial(rnd));
		}

		public static Game Create(Board board)
		{
			var game = new Game();
			game.m_Moves.Add(board);
			return game;
		}
	}
}
