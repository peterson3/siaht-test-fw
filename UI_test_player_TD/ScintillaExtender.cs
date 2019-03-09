
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//    ScintillaNET Editor extender for code intellisense display
//    Copyright 2015 Daniel Wagner O. de Medeiros
//
//    This file is part of DWSIM.
//
//    DWSIM is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    DWSIM is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with DWSIM.  If not, see <http://www.gnu.org/licenses/>.

using ScintillaNET;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.Drawing;

/// <summary>
/// ScintillaNET Editor extender for code intellisense display
/// </summary>
/// <remarks>(c) 2015 Daniel Medeiros</remarks>
static class ScintillaExtender
{

	/// <summary>
	/// Sets the editor style for Python language.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <param name="fontname">Name of the font to be used.</param>
	/// <param name="fontsize">Size of the font to be used.</param>
	/// <param name="viewspaces">Enables or disables whitspace highlighting.</param>
	/// <remarks></remarks>
	public static void SetEditorStyle(this ScintillaNET.Scintilla scintilla, string fontname, int fontsize, bool viewspaces)
	{
		scintilla.StyleResetDefault();
		scintilla.Styles[Style.Default].Font = fontname;
		scintilla.Styles[Style.Default].Size = fontsize;
		scintilla.StyleClearAll();

		// i.e. Apply to all
		// Set the lexer

		scintilla.Lexer = Lexer.Python;

		// Known lexer properties:
		// "tab.timmy.whinge.level",
		// "lexer.python.literals.binary",
		// "lexer.python.strings.u",
		// "lexer.python.strings.b",
		// "lexer.python.strings.over.newline",
		// "lexer.python.keywords2.no.sub.identifiers",
		// "fold.quotes.python",
		// "fold.compact",
		// "fold"

		// Some properties we like

		scintilla.SetProperty("tab.timmy.whinge.level", "1");
		scintilla.SetProperty("fold", "1");

		scintilla.Margins[0].Width = 20;

		// Use margin 2 for fold markers

		scintilla.Margins[1].Type = MarginType.Symbol;
		scintilla.Margins[1].Mask = Marker.MaskFolders;
		scintilla.Margins[1].Sensitive = true;
		scintilla.Margins[1].Width = 20;

		// Reset folder markers

		for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++) {
			scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
			scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
		}

		// Style the folder markers

		scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
		scintilla.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
		scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
		scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
		scintilla.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
		scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
		scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
		scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
		scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

		// Enable automatic folding

		scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

		// Set the styles

		scintilla.Styles[Style.Python.Default].ForeColor = Color.FromArgb(0x80, 0x80, 0x80);
		scintilla.Styles[Style.Python.CommentLine].ForeColor = Color.FromArgb(0x0, 0x7f, 0x0);
		scintilla.Styles[Style.Python.CommentLine].Italic = true;
		scintilla.Styles[Style.Python.Number].ForeColor = Color.FromArgb(0x0, 0x7f, 0x7f);
		scintilla.Styles[Style.Python.String].ForeColor = Color.FromArgb(0x7f, 0x0, 0x7f);
		scintilla.Styles[Style.Python.Character].ForeColor = Color.FromArgb(0x7f, 0x0, 0x7f);
		scintilla.Styles[Style.Python.Word].ForeColor = Color.FromArgb(0x0, 0x0, 0x7f);
		scintilla.Styles[Style.Python.Word].Bold = true;
		scintilla.Styles[Style.Python.Triple].ForeColor = Color.FromArgb(0x7f, 0x0, 0x0);
		scintilla.Styles[Style.Python.TripleDouble].ForeColor = Color.FromArgb(0x7f, 0x0, 0x0);
		scintilla.Styles[Style.Python.ClassName].ForeColor = Color.FromArgb(0x0, 0x0, 0xff);
		scintilla.Styles[Style.Python.ClassName].Bold = true;
		scintilla.Styles[Style.Python.DefName].ForeColor = Color.FromArgb(0x0, 0x7f, 0x7f);
		scintilla.Styles[Style.Python.DefName].Bold = true;
		scintilla.Styles[Style.Python.Operator].Bold = true;
		scintilla.Styles[Style.Python.CommentBlock].ForeColor = Color.FromArgb(0x7f, 0x7f, 0x7f);
		scintilla.Styles[Style.Python.CommentBlock].Italic = true;
		scintilla.Styles[Style.Python.StringEol].ForeColor = Color.FromArgb(0x0, 0x0, 0x0);
		scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xe0, 0xc0, 0xe0);
		scintilla.Styles[Style.Python.StringEol].FillLine = true;
                        
		scintilla.Styles[Style.Python.DefName].ForeColor = Color.Brown;
		scintilla.Styles[Style.Python.DefName].Bold = true;
                        
		scintilla.Styles[Style.Python.Word2].ForeColor = Color.DarkRed;
		scintilla.Styles[Style.Python.Word2].Bold = true;

		var _with1 = scintilla.Styles[Style.CallTip];
		_with1.Font = fontname;
		_with1.Size = fontsize - 2;
		_with1.ForeColor = Color.FromKnownColor(KnownColor.ActiveCaptionText);

		if (viewspaces)
			scintilla.ViewWhitespace = WhitespaceMode.VisibleAfterIndent;

		// Keyword lists:
		// 0 "Keywords",
		// 1 "Highlighted identifiers"

		dynamic python2 = "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
		dynamic python3 = "False None True and as assert break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield";

		//add keywords from DWSIM classes properties and methods

		string netprops = "";

		dynamic props = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.MaterialStream").GetProperties();
		foreach (var p_loopVariable in props) {
			var p = p_loopVariable;
			netprops += p.Name + " ";
		}
		dynamic methods = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.MaterialStream").GetMethods();
		foreach (var m_loopVariable in methods) {
			var m = m_loopVariable;
			netprops += m.Name + " ";
		}
		props = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.EnergyStream").GetProperties();
		foreach (var p_loopVariable in props) {
			var p = p_loopVariable;
			netprops += p.Name + " ";
		}
		methods = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.EnergyStream").GetMethods();
		foreach (var m_loopVariable in methods) {
			var m = m_loopVariable;
			netprops += m.Name + " ";
		}
		props = Type.GetType("DWSIM.FormFlowsheet").GetProperties();
		foreach (var p_loopVariable in props) {
			var p = p_loopVariable;
			if (p.PropertyType.Namespace != "System.Windows.Forms")
				netprops += p.Name + " ";
		}
		methods = Type.GetType("DWSIM.FormFlowsheet").GetMethods();
		foreach (var m_loopVariable in methods) {
			var m = m_loopVariable;
			netprops += m.Name + " ";
		}
		props = Type.GetType("DWSIM.SpreadsheetForm").GetProperties();
		foreach (var p_loopVariable in props) {
			var p = p_loopVariable;
			if (p.PropertyType.Namespace != "System.Windows.Forms")
				netprops += p.Name + " ";
		}
		methods = Type.GetType("DWSIM.SpreadsheetForm").GetMethods();
		foreach (var m_loopVariable in methods) {
			var m = m_loopVariable;
			netprops += m.Name + " ";
		}
		props = Type.GetType("DWSIM.DWSIM.SimulationObjects.PropertyPackages.PropertyPackage").GetProperties();
		foreach (var p_loopVariable in props) {
			var p = p_loopVariable;
			if (p.PropertyType.Namespace != "System.Windows.Forms")
				netprops += p.Name + " ";
		}
		methods = Type.GetType("DWSIM.DWSIM.SimulationObjects.PropertyPackages.PropertyPackage").GetMethods();
		foreach (var m_loopVariable in methods) {
			var m = m_loopVariable;
			netprops += m.Name + " ";
		}

		string objects = "";

		if ((Convert.ToInt32(scintilla.Tag)) == 1) {
			//editor is being used at flowsheet level.
			props = Type.GetType("DWSIM.DWSIM.Flowsheet.FlowsheetSolver").GetProperties();
			foreach (var p_loopVariable in props) {
				var p = p_loopVariable;
				if (p.PropertyType.Namespace != "System.Windows.Forms")
					netprops += p.Name + " ";
			}
			methods = Type.GetType("DWSIM.DWSIM.Flowsheet.FlowsheetSolver").GetMethods();
            foreach (var m_loopVariable in methods)
            {
                var m = m_loopVariable;
				netprops += m.Name + " ";
			}
			objects = "MaterialStream EnergyStream PropertyPackage UnitOp Flowsheet Spreadsheet Plugins Solver DWSIM";

		} else {
			//editor is being used at script unit operation level
			objects = "ims1 ims2 ims3 ims4 ims5 ims6 ies1 oms1 oms2 oms3 oms4 oms5 oms6 oes1 Flowsheet Spreadsheet Plugins Solver Me DWSIM";

		}

		scintilla.SetKeywords(0, python2 + " " + python3);
		scintilla.SetKeywords(1, objects + " " + netprops);

		ScintillaExtender.SetColumnMargins(scintilla);

	}

	/// <summary>
	/// Sets the column margins to correctly show line numbers.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <remarks></remarks>
	public static void SetColumnMargins(this ScintillaNET.Scintilla scintilla)
	{
		dynamic maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;

		const int padding = 2;
		scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;

	}
	
    /// <summary>
	/// Show an autocomplete listbox with methods and properties from the object with the entered keyword.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <remarks></remarks>
	public static void ShowAutoComplete(this ScintillaNET.Scintilla scintilla)
	{
		string suggestions = "";

		//parses the last keyword (object) (before the ".") and get suggestions for the autocomplete box from its properties and methods

        string [] splitters = {
			".",
			"(",
			")",
			" ",
			"\r",
			"\n",
			"\r\n"
		};

		dynamic text = ScintillaExtender.getLastWord(scintilla).Split(splitters, StringSplitOptions.RemoveEmptyEntries);
		dynamic lastchar = Convert.ToChar(scintilla.GetCharAt(scintilla.CurrentPosition));

        //text2 = text2.Remove(text2.Length - 2).Trim();
        string lastkeyword = "";


        if (text.Length >= 1)
        {
            if (text.Length >= 2)
            {
                lastkeyword = text[text.Length - 2].Trim();
                // System.Windows.MessageBox.Show("[1] " + lastkeyword);
            }
            else
            {
                lastkeyword = text[text.Length - 1].Trim();
                // System.Windows.MessageBox.Show("[2] " + lastkeyword);
            }

            switch (lastkeyword)
            {
                case "Browser":
                    dynamic props = typeof(TopDown_QA_FrameWork.Driver).GetProperties();
                    foreach (var p_loopVariable in props)
                    {
                        //System.Windows.Forms.MessageBox.Show(p_loopVariable.Name);
                        var p = p_loopVariable;
                        suggestions += (p.Name) + " ";
                    }

                    dynamic methods = typeof(TopDown_QA_FrameWork.Driver).GetMethods();
                    foreach (var m_loopVariable in methods)
                    {
                        var m = m_loopVariable;
                        suggestions += (m.Name) + " ";
                    }
                    break;

                default:
                    if ((Convert.ToInt32(scintilla.Tag)) == 1)
                    {
                        suggestions = "Browser teste1";
                    }
                    else
                    {
                        suggestions = "Browser teste2";
                    }
                    break;
            }
        }
        else
        {
            if (Convert.ToInt32(scintilla.Tag) == 1)
            {
                //editor is being used at flowsheet level.
                suggestions = "Browser teste3";
            }
            else
            {
                //editor is being used at script unit operation level
                suggestions = "Browser teste4 solver zinbu";
            }
        }

		dynamic currentPos = scintilla.CurrentPosition;
		dynamic wordStartPos = scintilla.WordStartPosition(currentPos, true);

		// Display the autocompletion list
		dynamic lenEntered = currentPos - wordStartPos;

        //System.Windows.Forms.MessageBox.Show(lenEntered.ToString() + " lastChar= " + lastchar );
        //System.Windows.Forms.MessageBox.Show(suggestions);
        //System.Windows.Forms.MessageBox.Show(lastkeyword.ToString());

        if (lenEntered > 0 & lastchar != '.')
        {
            scintilla.AutoCShow(lenEntered, suggestions);
        }
        else
        {

            if (lastchar == '.' & text.Length >= 1)
            {
                //System.Windows.Forms.MessageBox.Show(suggestions);
                scintilla.AutoCShow(0, suggestions.Trim());
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("lenEntered: " + lenEntered.ToString() + "; lastChar: " + lastchar.ToString() + "; text.length: " + text.Length.ToString());
            }
        }
	}

	/// <summary>
	/// Show a tooltip with information about the entered object method or property.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <param name="reader">Jolt's XmlDocCommentReader instance, to get and display comments from assembly-generated XML file.</param>
	/// <remarks></remarks>
	public static void ShowToolTip(this ScintillaNET.Scintilla scintilla, Jolt.XmlDocCommentReader reader)
	{
		//parses the last keyword (object) (before the ".") and get suggestions for the autocomplete box from its properties and methods
        
        string [] splitters = {
			".",
			"(",
			")",
			" ",
			"\r",
			"\n",
			"\r\n"
		};		
        
        dynamic text = ScintillaExtender.getLastWord(scintilla).Split(splitters, StringSplitOptions.RemoveEmptyEntries);
		dynamic lastchar = Convert.ToChar(scintilla.GetCharAt(scintilla.CurrentPosition));

		string helptext = "";

		if (text.Length >= 2) {
			string lastkeyword = text[text.Length - 1];
            string lastobj = text[text.Length - 2].Trim();
            //string obj = Convert.ToString(lastobj));
            switch (lastobj)
            {
                case "Browser":
                    dynamic prop = typeof(TopDown_QA_FrameWork.Driver).GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla, prop[0], reader);
					break;
				case "ims1":
				case "ims2":
				case "ims3":
				case "ims4":
				case "ims5":
				case "ims6":
				case "oms1":
				case "oms2":
				case "oms3":
				case "oms4":
				case "oms5":
				case "MaterialStream":
					prop = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.MaterialStream").GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla,prop[0], reader);
					break;
				case "ies1":
				case "oes1":
				case "EnergyStream":
					prop = Type.GetType("DWSIM.DWSIM.SimulationObjects.Streams.EnergyStream").GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla, prop[0], reader);
					break;
				case "Flowsheet":
					prop = Type.GetType("DWSIM.FormFlowsheet").GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla, prop[0], reader);
					break;
				case "Spreadsheet":
					prop = Type.GetType("DWSIM.SpreadsheetForm").GetMember(lastkeyword);
					if (prop.Length > 0)
                        helptext = ScintillaExtender.FormatHelpTip(scintilla, prop[0], reader);
					break;
				case "PropertyPackage":
					prop = Type.GetType("DWSIM.DWSIM.SimulationObjects.PropertyPackages.PropertyPackage").GetMember(lastkeyword);
					if (prop.Length > 0)
                        helptext = ScintillaExtender.FormatHelpTip(scintilla, prop[0], reader);
					break;
				case "UnitOp":
				case "Me":
					prop = Type.GetType("DWSIM.SimulationObjects_UnitOpBaseClass").GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla, prop(0), reader);
					break;
				case "Solver":
					prop = Type.GetType("DWSIM.DWSIM.Flowsheet.FlowsheetSolver").GetMember(lastkeyword);
					if (prop.Length > 0)
						helptext = ScintillaExtender.FormatHelpTip(scintilla, prop(0), reader);
					break;
			}

			//shows the tooltip

			if (!string.IsNullOrEmpty(helptext))
				scintilla.CallTipShow(scintilla.CurrentPosition, helptext);
			else
				scintilla.CallTipCancel();


		} else {
			//hides tooltip if visible

			scintilla.CallTipCancel();

		}

	}

	/// <summary>
	/// Formats the text to be displayed in the tooltip using information from the object's member and from the XML documentation, if existing.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <param name="member">Reflected member to display information from.</param>
	/// <param name="reader">Jolt's XmlDocCommentReader instance, to get and display comments from assembly-generated XML file.</param>
	/// <returns>The formatted text to display in the tooltip.</returns>
	/// <remarks></remarks>
	private static string FormatHelpTip(this ScintillaNET.Scintilla scintilla, MemberInfo member, Jolt.XmlDocCommentReader reader)
	{

		switch (member.MemberType) {

			case MemberTypes.Method:

				dynamic methods = Type.GetType(member.DeclaringType.FullName).GetMethods().Where(m => m.Name == member.Name).ToList();
				dynamic method = methods[0];

				string summary = "";
				string returntype = "";
				string returndescription = "";
				string remarks = "";

				Dictionary<string, string> argumentdescriptions = new Dictionary<string, string>();

				string txthelp = method.DeclaringType.Name + " method '" + member.Name + "'" + "\r\n";

				dynamic xmlhelp = reader.GetComments(method);

				if ((xmlhelp != null)) {
					dynamic @params = xmlhelp.Elements("param").ToList;
					foreach (var p_loopVariable in @params) {
						var p = p_loopVariable;
						if (p.Value.ToString.Length > 70) {
							argumentdescriptions.Add(p.Attribute("name"), p.Value.ToString.Substring(0, 70).Trim("\n") + " [...]");
						} else {
                            argumentdescriptions.Add(p.Attribute("name"), p.Value.ToString.Trim("\r\n"));
						}
					}
					if (method.ReturnType.Name != "Void") {
						dynamic rdesc = xmlhelp.Elements("returns").FirstOrDefault;
						if ((rdesc != null)) {
							returndescription = rdesc.Value;
						}
					}
					dynamic redesc = xmlhelp.Elements("remarks").FirstOrDefault;
					if ((redesc != null)) {
						if (redesc.Value.Length > 1000) {
							remarks = redesc.Value.Substring(0, 1000) + " [...]";
						} else {
							remarks = redesc.Value;
						}
					}
					redesc = xmlhelp.Elements("summary").FirstOrDefault;
					if ((redesc != null)) {
						summary = redesc.Value;
						txthelp += summary + "\r\n";
					}
				}

				if (method.GetParameters.Count > 0) {
					txthelp += "Parameters:" + "\r\n" + "\r\n";
					txthelp += "Type".PadRight(18) + "Name".PadRight(15) + "Description" + "\r\n";
					foreach (var par_loopVariable in method.GetParameters) {
						var par = par_loopVariable;
						if (argumentdescriptions.ContainsKey(par.Name)) {
                            txthelp += par.ParameterType.Name.PadRight(18) + par.Name.PadRight(15) + argumentdescriptions[par.Name] + "\r\n";
						} else {
                            txthelp += par.ParameterType.Name.PadRight(18) + par.Name.PadRight(15) + "\r\n";
						}
					}
                    txthelp += "\r\n";
				}

				txthelp += "Return Type: " + method.ReturnType.ToString;
				if (!string.IsNullOrEmpty(returndescription))
                    txthelp += "\r\n" + "Return Parameter Description: " + returndescription;
				if (!string.IsNullOrEmpty(remarks))
                    txthelp += "\r\n" + "\r\n" + "Remarks: " + remarks;


				return txthelp;
			case MemberTypes.Property:

				dynamic props = Type.GetType(member.DeclaringType.FullName).GetProperties().Where(p => p.Name == member.Name).ToList();
				dynamic prop = props(0);

				summary = "";
				string proptype = "";

                txthelp = prop.DeclaringType.Name + " property '" + prop.Name + "'" + "\r\n";
				txthelp += "Type: " + prop.PropertyType.ToString;

				xmlhelp = reader.GetComments(prop);

				if ((xmlhelp != null)) {
					dynamic redesc = xmlhelp.Elements("summary").FirstOrDefault;
					if ((redesc != null)) {
                        txthelp += "\r\n" + "Description: " + redesc.Value;
					}
				}


				return txthelp;
			default:


				return "";
		}

	}

	/// <summary>
	/// Returns the last typed word in the editor.
	/// </summary>
	/// <param name="scintilla"></param>
	/// <returns></returns>
	/// <remarks></remarks>
	private static string getLastWord(this ScintillaNET.Scintilla scintilla)
	{

		string word = "";

		int pos = scintilla.SelectionStart;

		if (pos > 1) {
			string tmp = "";
			char f = new char();
			while (f != ' ' & pos > 0) {
				pos -= 1;
				tmp = scintilla.Text.Substring(pos, 1);
				f = Convert.ToChar(tmp[0]);
				word += f;
			}

			char[] ca = word.ToCharArray();
			Array.Reverse(ca);

			word = new String(ca);
		}
		return word.Trim();

	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
