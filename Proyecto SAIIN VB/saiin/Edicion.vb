Imports System.Runtime.InteropServices
'libreria'
Public Class Edicion
    'variables para x y y en el plano cartesiano'
    Dim valor As Boolean = False
    Dim piaval As Integer
    Dim yar As Integer

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(LR As Integer, TR As Integer, RR As Integer, BR As Integer, WE As Integer, HE As Integer) As IntPtr
    End Function
    Private Sub Edicion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 20, 20))
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        MsgBox("holi")
        End



    End Sub
End Class