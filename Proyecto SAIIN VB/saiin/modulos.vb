Imports System.Runtime.InteropServices
Public Class modulos

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(LR As Integer, TR As Integer, RR As Integer, BR As Integer, WE As Integer, HE As Integer) As IntPtr
    End Function

#Region "Variables"
    'Variable validacion de siguiente modulo
    Public valorModulo As Integer
    'variables para x y y en el plano cartesiano'
    Dim valor As Boolean = False
    Dim piaval As Integer
    Dim yar As Integer
#End Region

#Region "Evento Load y FormClose"
    Private Sub modulos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'no mover nada, esta linea de codigo se encarga de dar el tamano de la ventana y evitar que se vea reducida'
        'Me.StartPosition = StartPosition.CenterScreen
        'Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        'Me.Size = Screen.PrimaryScreen.WorkingArea.Size
        reconectar() 'Reconecta la BD nuevamente
        Me.Size = New Size(1262, 674)
        'evento para la forma redondeada'
        Me.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 20, 20))
    End Sub

    Private Sub modulos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        conexion.Close()
    End Sub
#End Region

#Region "Eventos"
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

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs)
        TabControl1.SelectedTab = TabControl1.TabPages.Item(0)
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs)
        TabControl1.SelectedTab = TabControl1.TabPages.Item(1)
    End Sub
#End Region

#Region "Botónes"
    'Guarda datos en la tabla de modulo 1 y pasa al siguiente
    Private Sub guardar_m1_Click(sender As Object, e As EventArgs) Handles guardar_m1.Click
        VerificarCamposM1()
    End Sub

    Private Sub cerrar_Click(sender As Object, e As EventArgs) Handles cerrar.Click
        If (MsgBox("¿Desea salir del programa?", 33, "Salir") = 1) Then
            Me.Close()
            End
        End If
    End Sub

    Private Sub m3PictureBox1_Click(sender As Object, e As EventArgs) Handles m3PictureBox1.Click
        m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(1)
    End Sub

    Private Sub m3PictureBox2_Click(sender As Object, e As EventArgs) Handles m3PictureBox2.Click
        m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(0)
    End Sub

    Private Sub m3PictureBox3_Click(sender As Object, e As EventArgs) Handles m3PictureBox3.Click
        m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(2)
    End Sub

    Private Sub m3PictureBox4_Click(sender As Object, e As EventArgs) Handles m3PictureBox4.Click
        m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(1)
    End Sub

    Private Sub m2PictureBox2_Click(sender As Object, e As EventArgs) Handles m2PictureBox2.Click
        m2TabControl1.SelectedTab = m2TabControl1.TabPages.Item(0)
    End Sub

    Private Sub m2PictureBox1_Click(sender As Object, e As EventArgs)
        m2TabControl1.SelectedTab = m2TabControl1.TabPages.Item(1)
    End Sub

    Private Sub m6PictureBox2_Click(sender As Object, e As EventArgs) Handles m6PictureBox2.Click
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(0)
    End Sub

    Private Sub m6PictureBox1_Click(sender As Object, e As EventArgs) Handles m6PictureBox1.Click
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(1)
    End Sub

    Private Sub minimizar_Click(sender As Object, e As EventArgs) Handles minimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub home_Click(sender As Object, e As EventArgs) Handles home.Click
        principal.Show()
        Me.Hide()
    End Sub

    Private Sub m2next1_Click(sender As Object, e As EventArgs) Handles m2next1.Click
        m2TabControl1.SelectedTab = m2TabControl1.TabPages(1)
    End Sub

    Private Sub m2next_Click(sender As Object, e As EventArgs) Handles m2next.Click
        m2TabControl1.SelectedTab = m2TabControl1.TabPages(2)
    End Sub

    Private Sub m2_anterior_PictuBox_Click(sender As Object, e As EventArgs) Handles m2_anterior_PictuBox.Click
        m2TabControl1.SelectedTab = m2TabControl1.TabPages(1)
    End Sub

    Private Sub next1_m2_Click(sender As Object, e As EventArgs) Handles next1_m2.Click
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(1)
    End Sub

    Private Sub anterior1_m2_Click(sender As Object, e As EventArgs)
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(0)
    End Sub

    Private Sub anterior2_m6_Click(sender As Object, e As EventArgs)
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(1)
    End Sub

    Private Sub next2_m2_Click(sender As Object, e As EventArgs)
        m6TabControl1.SelectedTab = m6TabControl1.TabPages.Item(2)
    End Sub

    Private Sub next1_m5_Click(sender As Object, e As EventArgs) Handles next1_m5.Click
        m5TabControl.SelectedTab = m5TabControl.TabPages.Item(1)
    End Sub

    Private Sub next2_m5_Click(sender As Object, e As EventArgs) Handles next2_m5.Click
        m5TabControl.SelectedTab = m5TabControl.TabPages.Item(2)
    End Sub

    Private Sub anterior1_m5_Click(sender As Object, e As EventArgs) Handles anterior1_m5.Click
        m5TabControl.SelectedTab = m5TabControl.TabPages.Item(0)
    End Sub

    Private Sub anterior2_m5_Click(sender As Object, e As EventArgs) Handles anterior2_m5.Click
        m5TabControl.SelectedTab = m5TabControl.TabPages.Item(1)
    End Sub

    Private Sub guardar_m2_Click(sender As Object, e As EventArgs) Handles guardar_m2.Click
        Edicion.Show()
        Me.Hide()
    End Sub

    Private Sub guardar_m3_Click(sender As Object, e As EventArgs) Handles guardar_m3.Click
        Edicion.Show()
        Me.Hide()
    End Sub

    Private Sub btn_guardar_m4_Click(sender As Object, e As EventArgs) Handles btn_guardar_m4.Click
        Edicion.Show()
        Me.Hide()
    End Sub

    Private Sub guardarM5_Click(sender As Object, e As EventArgs) Handles guardarM5.Click
        Edicion.Show()
        Me.Hide()
    End Sub

    Private Sub guardar_m5_Click(sender As Object, e As EventArgs) Handles guardar_m5.Click
        Edicion.Show()
        Me.Hide()
    End Sub

    Private Sub guardar_m6_Click(sender As Object, e As EventArgs)
        Edicion.Show()
        Me.Hide()
    End Sub

#End Region

#Region "Temporizadores"

#End Region

#Region "Métodos y Funciones"
    Sub VerificarCamposM1()
        'Variables locales para transformar y capturar
        Dim idea As Integer
        Dim fondos, experiencia, habilidad, disponibilidad As Boolean
        Dim nacimiento, motivo, quienes, beneficios, diferencias, idProyecto, problematica As String

        'Validaciones
        If (producto_RadioBtn_m1.Checked = False) And (servicio_RadioBtn_m1.Checked = False) Then
            MsgBox("Seleccione si su idea es un producto o servicio.", 16, "Llenar campo")
        ElseIf tbx1_m1.Text = "" Then
            MsgBox("Escriba de donde nacio su idea.", 16, "Llenar campo")
            tbx1_m1.Focus()
        ElseIf tbx2_m1.Text = "" Then
            MsgBox("Escriba lo que le motivo a crear su negocio", 16, "Llenar campo")
            tbx2_m1.Focus()
        ElseIf tbx3_m1problematica_idea.Text = "" Then
            MsgBox("Describe que problematica resuelve tu idea", 16, "Llenar campo")
            tbx3_m1problematica_idea.Focus()
        ElseIf tbx4_m1_quien.Text = "" Then
            MsgBox("Escriba quien tiene el problema", 16, "Llenar campo")
            tbx4_m1_quien.Focus()
        ElseIf tbx5_m1_beneficios.Text = "" Then
            MsgBox("Escriba lo que beneficios aporta tu idea", 16, "Llenar campo")
            tbx5_m1_beneficios.Focus()
        ElseIf tbx6_m1_ofrece.Text = "" Then
            MsgBox("Escribe lo que ofreces diferente de tu competencia", 16, "Llenar campo")
            tbx6_m1_ofrece.Focus()
        ElseIf (si_RadioBtn_tiempo_m1.Checked = False) And (no_RadioBtn_tiempo_m1.Checked = False) Then
            MsgBox("Seleccione si cuenta o no cuenta con disponibilidad de tiempo", 16, "Llenar campo")
        ElseIf (si_RadioBtn_experiencia_m1.Checked = False) And (no_RadioBtn_experiencia_m1.Checked = False) Then
            MsgBox("Seleccione si cuenta o no cuenta con experiencia en este tipo de negocios", 16, "Llenar campo")
        ElseIf (si_RadioBtn_habilidad_m1.Checked = False) And (no_RadioBtn_habilidad_m1.Checked = False) Then
            MsgBox("Seleccione si cuenta o no habilidad para llevar a cabo su idea", 16, "Llenar campo")
        ElseIf (si_RadioBtn_fondo_m1.Checked = False) And (no_RadioBtn_fondo_m1.Checked = False) Then
            MsgBox("Seleccione si cuenta o no fondos para desarrollar su idea", 16, "Llenar campo")
        Else
#Region "Valores"
            'Transforma los valores de acuerdo al tipo de dato en la BD para cada campo
            If producto_RadioBtn_m1.Checked = True Then
                idea = 1
            Else
                idea = 2
            End If

            If si_RadioBtn_fondo_m1.Checked = True Then
                fondos = True
            Else
                fondos = False
            End If

            If si_RadioBtn_experiencia_m1.Checked = True Then
                experiencia = True
            Else
                experiencia = False
            End If

            If si_RadioBtn_habilidad_m1.Checked = True Then
                habilidad = True
            Else
                habilidad = False
            End If

            If si_RadioBtn_tiempo_m1.Checked = True Then
                disponibilidad = True
            Else
                disponibilidad = False
            End If

            nacimiento = tbx1_m1.Text
            motivo = tbx2_m1.Text
            quienes = tbx4_m1_quien.Text
            beneficios = tbx5_m1_beneficios.Text
            diferencias = tbx6_m1_ofrece.Text
            problematica = tbx3_m1problematica_idea.Text

            idProyecto = idGlobal 'Baja el IDGLobal y lo guarda en idproyecto de este formulario



#End Region
            'poner metodo agregar como sql
            comando = New OleDb.OleDbCommand("INSERT INTO modulo1 VALUES(@idea, @fondos, @nacimiento, @motivo, @quienes, @beneficios, @diferencias, @experiencia, @habilidad, @disponibilidad, @idProyecto, @problematica)", conexion)
            comando.Parameters.AddWithValue("@idea", idea)
            comando.Parameters.AddWithValue("@fondos", fondos)
            comando.Parameters.AddWithValue("@nacimiento", nacimiento)
            comando.Parameters.AddWithValue("@motivo", motivo)
            comando.Parameters.AddWithValue("@quienes", quienes)
            comando.Parameters.AddWithValue("@beneficios", beneficios)
            comando.Parameters.AddWithValue("@diferencias", diferencias)
            comando.Parameters.AddWithValue("@experiencia", experiencia)
            comando.Parameters.AddWithValue("@habilidad", habilidad)
            comando.Parameters.AddWithValue("@disponibilidad", disponibilidad)
            comando.Parameters.AddWithValue("@idProyecto", idProyecto)
            comando.Parameters.AddWithValue("@problematica", problematica)
            comando.ExecuteNonQuery() 'Crea registro -> modulo 1
            TabControl1.SelectedIndex = 1
        End If
    End Sub
#End Region



End Class