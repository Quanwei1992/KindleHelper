Imports KindleHelper.Plugin.Interface
Imports System.Windows.Forms
Public Class VBPlugin
    Implements IPlugin
    Dim _watch As System.Diagnostics.Stopwatch = New System.Diagnostics.Stopwatch()
    Dim f As Form = New Form()
    Public Function getresult() As Object Implements [Interface].IPlugin.getresult
        Return "666"
    End Function
    Public Sub New()
        _watch.Start()
        Dim b As Button = New Button()
        With b
            .Text = "test"
            .Location = New System.Drawing.Point(f.Width / 2, f.Height / 2)

        End With
        AddHandler b.Click, AddressOf DDo
        f.Controls.Add(b)
        _watch.Stop()
    End Sub
    Private Sub DDo(ByVal sender As Object, ByVal e As EventArgs)
        MessageBox.Show("test")
    End Sub
    Public Function run() As Object Implements [Interface].IPlugin.run
        Return "1223"
    End Function

    Public Function Show() As String Implements [Interface].IPlugin.Show
        Return "VBPlugin"
    End Function

    Public Sub ShowForm() Implements [Interface].IPlugin.ShowForm
        f.Show()
    End Sub

    Public Sub ShowFormAsDialog() Implements [Interface].IPlugin.ShowFormAsDialog
        f.Visible = False
        f.ShowDialog()
    End Sub

    Public Function version() As String Implements [Interface].IPlugin.version
        Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Function

    Public Function Watch() As System.Diagnostics.Stopwatch Implements [Interface].IPlugin.Watch
        Return _watch
    End Function
    Public Function IsNotNeedToShowAsDialog() As Boolean Implements [Interface].IPlugin.IsNotNeedToShowAsDialog
        Return False
    End Function
End Class
