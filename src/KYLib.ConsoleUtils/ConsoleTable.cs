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
		private string _string = null;

		/// <summary>
		/// Indica sie s necesario actualziar la cadena.
		/// </summary>
		private bool _requireUpdate = true;

		/// <summary>
		/// Guarda las cadenas para separar filas.
		/// </summary>
		private string _separator = null;

		/// <summary>
		/// Guarda todo el contenido de la tabla.
		/// </summary>
		private readonly List<List<string>> _dic = new();

		/// <summary>
		/// Guarda los tamaños de cada columna.
		/// </summary>
		private readonly List<Int> _widths = new();

		/// <summary>
		/// Agregar columna interna
		/// </summary>
		private void AddColumn(string title, bool calc)
		{
			Int count = _dic.Count;
			_dic.Add(new List<string>());
			_dic[count].Add(title);
			if (calc)
				CalculateColumnsWidth();
			_requireUpdate = true;
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
			for (Int i = 0; i < _dic.Count; i++)
			{
				try
				{
					_dic[i].Add(content?[i]?.ToString());
				}
				catch (Exception)
				{
					_dic[i].Add(string.Empty);
				}
			}
			CalculateColumnsWidth();
			_requireUpdate = true;
		}

		/// <summary>
		/// Actualiza la representación en cadena de la tabla.
		/// </summary>
		private void UpdateString()
		{
			_string = string.Empty;
			var totalWidth = CalculateWidth();
			_separator = "-".Repeat(totalWidth);

			List<string> lines = new();
			_string += _separator + Environment.NewLine;
			Int count;
			var titles = true;
			// primero rellenamos las lineas
			for (Int item = 0; item < _dic.Count; item++)
			{
				count = 0;
				foreach (var row in _dic[item])
				{
					if (titles)
						lines.Add($"| {Fill(row, item)} |");
					else
						lines[count] += $" {Fill(row, item)} |";
					count++;
				}
				titles = false;
			}
			lines.Insert(1, _separator);
			//agregamos las lineas
			_string += lines.ToString(false, true) + Environment.NewLine;
			_string += _separator;
		}

		/// <summary>
		/// Rellena los contenidos d elas filas con espacios
		/// </summary>
		private string Fill(string row, Int index)
		{
			var dev = row;
			dev += " ".Repeat(_widths[index] - dev.Length);
			return dev;
		}

		/// <summary>
		/// Calcula el tamaño maximo de cada columna.
		/// </summary>
		private void CalculateColumnsWidth()
		{
			_widths.Clear();
			foreach (var item in _dic)
			{
				Int maxwidth = 0;
				item.ForEach(i =>
				{
					if (i.Length > maxwidth)
						maxwidth = i.Length;
				});
				_widths.Add(maxwidth);
			}
		}

		/// <summary>
		/// Calcula el tamaño de toda la tabla.
		/// </summary>
		private Int CalculateWidth()
		{
			Int dev = 1;
			foreach (var item in _widths)
				dev += item + 3;
			return dev;
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			if (_requireUpdate) UpdateString();
			_requireUpdate = false;
			return _string;
		}
	}
}