Imports System.Runtime.InteropServices
Public Class inicio
    Dim valor As Boolean = False
    Dim piaval As Integer
    Dim yar As Integer
    Dim contador As Integer

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(LR As Integer, TR As Integer, RR As Integer, BR As Integer, WE As Integer, HE As Integer) As IntPtr
    End Function
    Private Sub inicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GunaCircleProgressBar1.Value = 0
        Me.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 20, 20))

        Panelogo.Location = New Point((Width - Panelogo.Width) / 2, (Height - Panelogo.Height) / 2)
        Timer1.Start()
    End Sub
    ' movimiento del panel con el raton'
    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        valor = True
        piaval = Cursor.Position.X - Me.Left
        yar = Cursor.Position.Y - Me.Top

    End Sub
    'paso 2'
    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If valor = True Then
            Me.Top = Cursor.Position.Y - yar
            Me.Left = Cursor.Position.X - piaval

        End If

    End Sub
    'paso3'
    Private Sub Panel1_MouseUP(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        valor = False


    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        GunaCircleProgressBar1.Increment(16)
        contador += 1
        If contador = 20 Then
            Timer1.Stop()
            Me.Hide()
            principal.Show()
        End If
    End Sub

    Private Sub Panelogo_Paint(sender As Object, e As PaintEventArgs) Handles Panelogo.Paint

    End Sub

    Private Sub GunaCircleProgressBar1_Click(sender As Object, e As EventArgs) Handles GunaCircleProgressBar1.Click

    End Sub
End Class