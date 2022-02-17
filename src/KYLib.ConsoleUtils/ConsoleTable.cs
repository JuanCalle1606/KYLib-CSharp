using System;
using System.Collections.Generic;
using KYLib.Extensions;
using KYLib.MathFn;

namespace KYLib.ConsoleUtils
{
	/// <summary>
	/// Representa una tabla que puede ser convertida en cadena para mostrarla por consola.
	/// </summary>
	public sealed class ConsoleTable
	{
		/// <summary>
		/// Guarda la representación en cadena de esta tabla.
		/// </summary>
		private string String = null;

		/// <summary>
		/// Indica sie s necesario actualziar la cadena.
		/// </summary>
		private bool RequireUpdate = true;

		/// <summary>
		/// Guarda las cadenas para separar filas.
		/// </summary>
		private string Separator = null;

		/// <summary>
		/// Guarda todo el contenido de la tabla.
		/// </summary>
		private readonly List<List<string>> Dic = new();

		/// <summary>
		/// Guarda los tamaños de cada columna.
		/// </summary>
		private readonly List<Int> Widths = new();

		/// <summary>
		/// Agregar columna interna
		/// </summary>
		private void AddColumn(string title, bool calc)
		{
			Int count = Dic.Count;
			Dic.Add(new List<string>());
			Dic[count].Add(title);
			if (calc)
				CalculateColumnsWidth();
			RequireUpdate = true;
		}

		/// <summary>
		/// Agrega una columna a la tabla.
		/// </summary>
		/// <param name="title">Titulo que tendra la columna.</param>
		public void AddColumn(string title) => AddColumn(title, true);

		/// <summary>
		/// Agrega un conjunto de columnas a la tabla.
		/// </summary>
		/// <param name="titles">Titulos que tendran cada columna.</param>
		public void AddColumns(params string[] titles)
		{
			foreach (var item in titles)
				AddColumn(item, false);
			CalculateColumnsWidth();
		}

		/// <summary>
		/// Agrega una nueva fila a la tabla.
		/// </summary>
		/// <param name="content">Lista de objetos que seran ingresados en cada columna, si se pasan mas objetos que la cantidad de columnas entonces seran ignorados los pasados y si se pasan menos entonces los que falten seran añadidos como cadenas vacias.</param>
		public void AddRow(params object[] content)
		{
			for (Int i = 0; i < Dic.Count; i++)
			{
				try
				{
					Dic[i].Add(content?[i]?.ToString());
				}
				catch (Exception)
				{
					Dic[i].Add(string.Empty);
				}
			}
			CalculateColumnsWidth();
			RequireUpdate = true;
		}

		/// <summary>
		/// Actualiza la representación en cadena de la tabla.
		/// </summary>
		private void UpdateString()
		{
			String = string.Empty;
			var totalWidth = CalculateWidth();
			Separator = "-".Repeat(totalWidth);

			List<string> lines = new();
			String += Separator + Environment.NewLine;
			Int count;
			var titles = true;
			// primero rellenamos las lineas
			for (Int item = 0; item < Dic.Count; item++)
			{
				count = 0;
				foreach (var row in Dic[item])
				{
					if (titles)
						lines.Add($"| {Fill(row, item)} |");
					else
						lines[count] += $" {Fill(row, item)} |";
					count++;
				}
				titles = false;
			}
			lines.Insert(1, Separator);
			//agregamos las lineas
			String += lines.ToString(false, true) + Environment.NewLine;
			String += Separator;
		}

		/// <summary>
		/// Rellena los contenidos d elas filas con espacios
		/// </summary>
		private string Fill(string row, Int index)
		{
			var dev = row;
			dev += " ".Repeat(Widths[index] - dev.Length);
			return dev;
		}

		/// <summary>
		/// Calcula el tamaño maximo de cada columna.
		/// </summary>
		private void CalculateColumnsWidth()
		{
			Widths.Clear();
			foreach (var item in Dic)
			{
				Int maxwidth = 0;
				item.ForEach(i =>
				{
					if (i.Length > maxwidth)
						maxwidth = i.Length;
				});
				Widths.Add(maxwidth);
			}
		}

		/// <summary>
		/// Calcula el tamaño de toda la tabla.
		/// </summary>
		private Int CalculateWidth()
		{
			Int dev = 1;
			foreach (var item in Widths)
				dev += item + 3;
			return dev;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			if (RequireUpdate) UpdateString();
			RequireUpdate = false;
			return String;
		}
	}
}