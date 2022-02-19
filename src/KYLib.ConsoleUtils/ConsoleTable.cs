using System;
using System.Collections.Generic;
using KYLib.Extensions;
namespace KYLib.ConsoleUtils;

/// <summary>
/// Representa una tabla que puede ser convertida en cadena para mostrarla por consola.
/// </summary>
public sealed class ConsoleTable
{
	/// <summary>
	/// Guarda la representación en cadena de esta tabla.
	/// </summary>
	string _string = string.Empty;

	/// <summary>
	/// Indica sie s necesario actualziar la cadena.
	/// </summary>
	bool _requireUpdate = true;

	/// <summary>
	/// Guarda las cadenas para separar filas.
	/// </summary>
	string _separator = string.Empty;

	/// <summary>
	/// Guarda el contenido de la tabla.
	/// </summary>
	readonly List<List<string?>> _dic = new();

	/// <summary>
	/// Guarda los tamaños de cada columna.
	/// </summary>
	readonly List<kint> _widths = new();

	/// <summary>
	/// Agregar columna interna
	/// </summary>
	void AddColumn(string title, bool calc)
	{
		kint count = _dic.Count;
		_dic.Add(new List<string?>());
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
	public void AddRow(params object?[] content)
	{
		for (kint i = 0; i < _dic.Count; i++)
		{
			try
			{
				_dic[i].Add(content[i]?.ToString());
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
	void UpdateString()
	{
		_string = string.Empty;
		var totalWidth = CalculateWidth();
		_separator = "-".Repeat(totalWidth);

		List<string> lines = new();
		_string += _separator + Environment.NewLine;
		var titles = true;
		// primero rellenamos las lineas
		for (kint item = 0; item < _dic.Count; item++)
		{
			kint count = 0;
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
	string Fill(string? row, kint index)
	{
		var dev = row;
		dev += " ".Repeat(_widths[index] - (dev?.Length ?? 0));
		return dev;
	}

	/// <summary>
	/// Calcula el tamaño maximo de cada columna.
	/// </summary>
	void CalculateColumnsWidth()
	{
		_widths.Clear();
		foreach (var item in _dic)
		{
			kint maxwidth = 0;
			item.ForEach(i =>
			{
				if (i != null && i.Length > maxwidth)
					maxwidth = i.Length;
			});
			_widths.Add(maxwidth);
		}
	}

	/// <summary>
	/// Calcula el tamaño de toda la tabla.
	/// </summary>
	kint CalculateWidth()
	{
		kint dev = 1;
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