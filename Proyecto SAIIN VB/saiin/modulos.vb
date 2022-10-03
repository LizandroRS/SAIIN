Imports System.Runtime.InteropServices
Imports System.Data
Imports System.Data.OleDb
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
        verificar_permisos()

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



    Private Sub TabPage20_Click(sender As Object, e As EventArgs) Handles TabPage20.Click

    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBox179_TextChanged(sender As Object, e As EventArgs) Handles TextBox179.TextChanged

    End Sub

    Private Sub PictureBox51_Click(sender As Object, e As EventArgs) Handles PictureBox51.Click


    End Sub

    Private Sub PictureBox65_Click(sender As Object, e As EventArgs) Handles PictureBox65.Click
        contador()
        agregarempleados()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim file As New OpenFileDialog()
        file.Filter = "Archivo JPG|*.jpg|*.PNG|*.png"
        If file.ShowDialog() = DialogResult.OK Then
            PictureBoxLogo.Image = Image.FromFile(file.FileName)
        End If
    End Sub

    Private Sub PictureBoxLogo_Click(sender As Object, e As EventArgs) Handles PictureBoxLogo.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub cargarcanva_Click(sender As Object, e As EventArgs) Handles cargarcanva.Click
        Try
            System.Diagnostics.Process.Start("https://www.canva.com/es_mx/?_branch_match_id=1099513789461365275&_branch_referrer=H4sIAAAAAAAAA8soKSkottLXT07MK0vUy03Vz8jPTQUAfJSCVBUAAAA%3D")
        Catch ex As Exception
            MessageBox.Show("Pagina Web No Encontrada")
        End Try
    End Sub

    Sub VerificarCamposM3tab1()
        'Variables locales para transformar y capturar
        'Validaciones m3tab1
        If m3ComboBox1.Text = "" Then
            MsgBox("Seleccione el giro de su empresa.", 16, "Llenar campo")
            m3ComboBox1.Focus()
        ElseIf TextBox8.Text = "" Then
            MsgBox("Escriba el nombre de su empresa.", 16, "Llenar campo")
            TextBox18.Focus()
        ElseIf TextBox10.Text = "" Then
            MsgBox("Escriba la vision de su empresa", 16, "Llenar campo")
            TextBox110.Focus()
        ElseIf TextBox11.Text = "" Then
            MsgBox("Escriba la mision de su empreesa", 16, "Llenar campo")
            TextBox11.Focus()
        ElseIf TextBox12.Text = "" Then
            MsgBox("Escriba los valores de su empresa", 16, "Llenar campo")
            TextBox12.Focus()
        Else
            m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(1)
        End If
    End Sub
    Sub VerificarCamposM3tab2()
        'Validaciones m3tab2
        If m3ComboBox1.Text = "" Then
            MsgBox("Seleccione cuantos empleados son", 16, "Llenar campo")
            m3ComboBox1.Focus()
        ElseIf TextBox13.Text = "" Then
            MsgBox("Escriba el area del empleado", 16, "Llenar campo")
            TextBox13.Focus()
        ElseIf TextBox14.Text = "" Then
            MsgBox("Escribe el tipo de empleado", 16, "Llenar campo")
            TextBox14.Focus()
        ElseIf TextBox15.Text = "" Then
            MsgBox("Escribe el perfil del empleado", 16, "Llenar campo")
            TextBox15.Focus()
        ElseIf TextBox16.Text = "" Then
            MsgBox("Escribe las funciones del empleado", 16, "Llenar campo")
            TextBox16.Focus()
        ElseIf ComboBox17.Text = "" Then
            MsgBox("seleccione el turno del empleado", 16, "Llenar campo")
            ComboBox17.Focus()
        ElseIf TextBox18.Text = "" Then
            MsgBox("Escribe la hora de entrada del empleado", 16, "Llenar campo")
            TextBox18.Focus()
        ElseIf TextBox34.Text = "" Then
            MsgBox("Escribe la hora de salida del empleado", 16, "Llenar campo")
            TextBox34.Focus()
        ElseIf TextBox104.Text = "" Then
            MsgBox("Escribe el salario del empleado", 16, "Llenar campo")
            TextBox104.Focus()
        Else
            m3TabControl1.SelectedTab = m3TabControl1.TabPages.Item(2)
        End If
    End Sub

    Sub VerificarCamposM3tab3()
        'Validaciones m3tab3
        If TextBox22.Text = "" Then
            MsgBox("Enliste los recursos materiales de la empresa", 16, "Llenar campo")
            TextBox22.Focus()
        ElseIf TextBox23.Text = "" Then
            MsgBox("Enliste los recursos tecnicos de la empresa", 16, "Llenar campo")
            TextBox23.Focus()
        ElseIf ComboBox1.Text = "" Then
            MsgBox("seleccione el aporte del capital", 16, "Llenar campo")
            ComboBox1.Focus()
        ElseIf ComboBox2.Text = "" Then
            MsgBox("seleccione el aporte de los socios", 16, "Llenar campo")
            ComboBox2.Focus()
        ElseIf TextBox17.Text = "" Then
            MsgBox("Escriba cuantos socios seran", 16, "Llenar campo")
            TextBox17.Focus()
        ElseIf TextBox96.Text = "" Then
            MsgBox("Ingrese la tasa de interés", 16, "Llenar campo")
            TextBox96.Focus()
            'validacion a solo numerico la tasa de interes
        ElseIf PictureBoxOrganigrama.Image Is Nothing Then
            MessageBox.Show("Favor de añadir su organigrama", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        Else
            Try
                Dim ms = New System.IO.MemoryStream
                PictureBoxOrganigrama.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                conexion = New OleDbConnection(cs)
                conexion.Open()
                Dim cbn As String = "insert into administracionM3(giroEmpM3,desNomEmpM3,descVisionM3,misionM3,valoresM3,numEmpleadosM3,idEmpl,organigramaM3,recMatM3,recTecM3,apoCapM3,apoSociosM3,numSociosM3,tipoCreditoM3) Values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14)"
                comando = New OleDbCommand(cbn) 'IdProy
                comando.Parameters.AddWithValue("@d1", idGlobal) 'IdProy de la empresa
                comando.Parameters.AddWithValue("@d2", m3ComboBox1.Text) 'giro de la empresa
                comando.Parameters.AddWithValue("@d3", TextBox8.Text) ' descripcion del nombre
                comando.Parameters.AddWithValue("@d4", TextBox10.Text) 'descripcion de la vision
                comando.Parameters.AddWithValue("@d5", TextBox11.Text) 'descripcion de la mision 
                comando.Parameters.AddWithValue("@d6", TextBox12.Text) 'descripcion de la valores 
                comando.Parameters.AddWithValue("@d7", ComboBox11.Text) 'cuantos empleados son
                comando.Parameters.AddWithValue("@d8", Label11.Text) 'id empleados
                comando.Parameters.AddWithValue("@d9", ms.GetBuffer) 'organigrama
                comando.Parameters.AddWithValue("@d10", TextBox22.Text) ' recursos materiales
                comando.Parameters.AddWithValue("@d11", TextBox23.Text) ' recursos tecnicos
                comando.Parameters.AddWithValue("@d12", ComboBox1.Text) ' aporte capital
                comando.Parameters.AddWithValue("@d13", ComboBox2.Text) ' aporte socios
                comando.Parameters.AddWithValue("@d14", ComboBox1.Text) ' numero de socios
                comando.Parameters.AddWithValue("@d15", ComboBox1.Text) ' tipo de credito
                comando.Connection = conexion
                comando.ExecuteNonQuery()
                conexion.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Edicion.Show()
            Me.Hide()
        End If
    End Sub

    Sub agregarempleados()
        Try
            conexion = New OleDbConnection(cs)
            conexion.Open()
            Dim em3 As String = "insert into puestosM3(idEmpl,idProy,areaEmplM3,tipoEmplM3,perfilEmplM3,funcioneEmplM3,turnoEmplM3,sueldoEmplM3) Values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)"
            comando = New OleDbCommand(em3)
            comando.Parameters.AddWithValue("@d1", Label1.Text) 'id empleado
            comando.Parameters.AddWithValue("@d2", idGlobal) 'id de proyecto falta actualizar------
            comando.Parameters.AddWithValue("@d3", TextBox13.Text) 'area del empleado
            comando.Parameters.AddWithValue("@d4", TextBox14.Text) 'tipo del empleado
            comando.Parameters.AddWithValue("@d5", TextBox15.Text) 'perfil del empleado
            comando.Parameters.AddWithValue("@d6", TextBox16.Text) 'funciones del empleado
            comando.Parameters.AddWithValue("@d7", TextBox13.Text) 'turno del empleado
            'hora entrada d8
            'hora salida d9
            comando.Parameters.AddWithValue("@d10", TextBox13.Text) 'salario del empleado
            comando.Connection = conexion
            comando.ExecuteNonQuery()
            conexion.Close()
            Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub contador()
        Static n As Integer
        n += 1
        Label12.Text = n
        If Label12.Text = M3empladosnecesarios.Text Then
            PictureBox65.Enabled = False
        Else
            PictureBox65.Enabled = True
            limpiarcamposM3tab2()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim file As New OpenFileDialog()
        file.Filter = "Archivo PNG|*.png|*.JPG|*.jpg"
        If file.ShowDialog() = DialogResult.OK Then
            PictureBoxCanva.Image = Image.FromFile(file.FileName)
        End If
    End Sub

    Sub limpiarcamposM3tab2()
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        ComboBox17.Text = ""
        TextBox18.Text = ""
        TextBox34.Text = ""
        TextBox104.Text = ""
    End Sub

    ' Modula 4 

    Dim contM4, contPermisos As Integer


    Sub verificar_permisos()

        If contM4 = "0" Or "" And contPermisos = "0" Or "" Then
            MsgBox("No haz agregado los permisos y regimen", MsgBoxStyle.Critical, "ESPERA!")
        Else
            Edicion.Show()
            Me.Hide()

        End If
    End Sub
    Sub agregar_permisitos()



        If TextBox96.Text = "" And TextBox103.Text = "" Then
            MsgBox("LLenar por favor los permisos", MsgBoxStyle.Exclamation, "Vacio")
            TextBox96.Focus()

        Else


            Try
                conexion = New OleDbConnection(cs)
                conexion.Open()
                Dim pm4 As String = "INSERT INTO permisosM4 (nomb,desc) VALUES (@Tipo, @describe)"
                comando = New OleDbCommand(pm4)
                comando.Parameters.AddWithValue("@Tipo", TextBox96.Text) 'Tipo de permiso 
                comando.Parameters.AddWithValue("@describe", TextBox103.Text) 'Descripcion del permiso

                comando.Connection = conexion
                comando.ExecuteNonQuery()
                conexion.Close()
                contPermisos += 1
                Refresh()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                contPermisos = 0
            End Try
        End If
    End Sub
    Private Sub PictureBox25_Click(sender As Object, e As EventArgs) Handles PictureBox25.Click
        agregar_permisitos()
        agregar_inscripcion()


    End Sub

    Sub agregar_inscripcion()
        contM4 = 0
        If CB2.Text = "" Then
            MsgBox("Por favor elegir un regimen", MsgBoxStyle.Exclamation, "Vacio")
        Else

            Try
                conexion = New OleDbConnection(cs)
                conexion.Open()
                Dim insM4 As String = "INSERT INTO Modula4 (idproy, inscripcionesM4) VALUES (@idp, @inscrip)"
                comando = New OleDbCommand(insM4)
                comando.Parameters.AddWithValue("@idp", idGlobal) 'Id del proyecto?
                comando.Parameters.AddWithValue("@inscrip", CB2.Text) 'Descripcion del permiso

                comando.Connection = conexion
                comando.ExecuteNonQuery()
                conexion.Close()
                contM4 += 1

                Refresh()

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                contM4 = 0
            End Try

        End If
    End Sub

#End Region



End Class