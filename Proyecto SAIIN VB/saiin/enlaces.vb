Imports System.Data
Imports System.Data.OleDb
Module enlaces

    Public conexion As New OleDbConnection
    Public comando As New OleDbCommand
    Public rs As OleDbDataReader
    Public adaptador As OleDbDataAdapter
    Public idGlobal As String

    Sub enlace()
        Try
            conexion.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\SimuladorDatos.mdb"
            conexion.Open()

        Catch ex As Exception
            MsgBox("no conectado")
        End Try
    End Sub

    Sub reconectar()
        conexion.Close()
        conexion.Open()
    End Sub

End Module
