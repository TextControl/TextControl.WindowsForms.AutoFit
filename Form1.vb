Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TextControl1 As TXTextControl.TextControl
    Friend WithEvents RulerBar1 As TXTextControl.RulerBar
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TextControl1 = New TXTextControl.TextControl()
        Me.RulerBar1 = New TXTextControl.RulerBar()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.SuspendLayout()
        '
        'TextControl1
        '
        Me.TextControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextControl1.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.TextControl1.Location = New System.Drawing.Point(0, 25)
        Me.TextControl1.Name = "TextControl1"
        Me.TextControl1.Size = New System.Drawing.Size(696, 429)
        Me.TextControl1.TabIndex = 0
        Me.TextControl1.Text = "TextControl1"
        Me.TextControl1.UserNames = Nothing
        '
        'RulerBar1
        '
        Me.RulerBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RulerBar1.Location = New System.Drawing.Point(0, 0)
        Me.RulerBar1.Name = "RulerBar1"
        Me.RulerBar1.Size = New System.Drawing.Size(696, 25)
        Me.RulerBar1.TabIndex = 1
        Me.RulerBar1.Text = "RulerBar1"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2, Me.MenuItem3})
        Me.MenuItem1.Text = "Table"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Insert"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "AutoFit"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(696, 454)
        Me.Controls.Add(Me.TextControl1)
        Me.Controls.Add(Me.RulerBar1)
        Me.Menu = Me.MainMenu1
        Me.Name = "Form1"
        Me.Text = "TX Table AutoFit Sample"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TextControl1.RulerBar = RulerBar1
        TextControl1.Load(Application.StartupPath + "\default.rtf", TXTextControl.StreamType.RichTextFormat)

    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        TextControl1.Tables.Add(8, 3)
    End Sub

    Private Sub checkCellWidth()

        ' only, if the current input position is in a table and
        ' this table does not contain a nested table
        If TextControl1.Tables.GetItem() Is Nothing Then
            Exit Sub
        ElseIf TextControl1.Tables.GetItem().NestedTables.Count > 0 Then
            Exit Sub
        End If

        ' new array for the column width values
        Dim colWidths(TextControl1.Tables.GetItem().Columns.Count - 1) As Integer

        For Each tc As TXTextControl.TableCell In TextControl1.Tables.GetItem().Cells

            Dim textBounds As Integer = 0

            ' check the width of every line in a cell
            For i As Integer = tc.Start To tc.Start + tc.Length - 1
                ' pick width, if the current one is the longest
                If TextControl1.Lines.GetItem(i).TextBounds.Width > textBounds Then
                    textBounds = TextControl1.Lines.GetItem(i).TextBounds.Width
                End If
            Next

            ' pick the width, if it is the longest in the whole column
            If textBounds > colWidths.GetValue(tc.Column - 1) Then
                colWidths.SetValue(textBounds, tc.Column - 1)
            End If

        Next

        resizeTable(colWidths, TextControl1.Tables.GetItem())

    End Sub

    Private Sub resizeTable(ByVal colSize() As Integer, ByVal table As TXTextControl.Table)

        ' indexed variables
        Dim i As Integer
        Dim a As Integer

        ' resize the table due to the filled array
        For a = 1 To table.Columns.Count
            If colSize.GetValue(i) = 0 Then

            Else
                table.Columns.GetItem(a).Width = colSize.GetValue(i) + 200
            End If
            i += 1
        Next

    End Sub

    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        checkCellWidth()
    End Sub
End Class
