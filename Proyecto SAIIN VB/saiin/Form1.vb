Imports System.Runtime.InteropServices
Public Class principal

    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(LR As Integer, TR As Integer, RR As Integer, BR As Integer, WE As Integer, HE As Integer) As IntPtr
    End Function

#Region "Variables"
    Dim valor As Boolean = False
    Dim piaval As Integer
    Dim validacion As Boolean = False
    Dim IdEmprendedor, IdProyecto As String
    Dim yar, cont, i As Integer
    Dim listaCurp(5) As String
#End Region

#Region "Evento Load"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width - 2, Height - 2, 20, 20))
        Me.Size = New Size(1366, 766)
        enlace() 'Conectar BD        
        Randomize() 'Generar NumRandom para ID Emprendedor / ID Proyecto 
        TabControl1.SelectedIndex = 1 'Enfoca al registro de ciudadanos
    End Sub
#End Region

#Region "Eventos"
    'Consulta al participante por medio de la CURP
    Private Sub participante_grupo_KeyDown(sender As Object, e As KeyEventArgs) Handles participante_grupo.KeyDown
        If e.KeyData = Keys.Enter Then
            If Len(participante_grupo.Text) <> 18 Then
                MsgBox("Favor de llenar la curp correctamente", 16, "Llenar campo")
                participante_grupo.Focus()
            Else
                comando = New OleDb.OleDbCommand("SELECT count(*) FROM emprendedor WHERE curp = @curp", conexion)
                comando.Parameters.AddWithValue("@curp", participante_grupo.Text)
                If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
                    MsgBox("El usuario no existe, primero registrese como emprendedor", 16, "Llenar campo")
                    nombre.Focus()
                    nombrec.Focus()
                Else
                    'RESCATAR NOMBRE Y ID UNA VEZ QUE LE DE CLICK EN AGREGAR
                    comando = New OleDb.OleDbCommand("SELECT * FROM emprendedor WHERE curp = @curp", conexion)

                    comando.Parameters.AddWithValue("@curp", participante_grupo.Text)
                    rs = comando.ExecuteReader()
                    If rs.Read Then
                        nombreCompleto.Text = rs(1).ToString + " " + rs(2).ToString
                    End If
                End If
            End If
        End If
    End Sub

    'Alterna visibilidad de campos ciudadano y alumno
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles botonAlumno.CheckedChanged
        If botonAlumno.Checked = True Then 'Si es alumno, habilita campos y deshabilita ciudadano
            TabControl1.SelectedIndex = 0

            nombrec.Enabled = False
            apellidoc.Enabled = False
            ocupacionc.Enabled = False
            telefonoc.Enabled = False
            curpc.Enabled = False

            nombre.Enabled = True
            apellido.Enabled = True
            ocupacion.Enabled = True
            telefono.Enabled = True
            domicilio.Enabled = True
            curpAlumno.Enabled = True
            carrera.Enabled = True
            instituto.Enabled = True
            botonITSA.Checked = False
            botonITSA.Enabled = True

        ElseIf botonAlumno.Checked = False Then 'Si es ciudadano, habilita campos y deshabilita alumno
            TabControl1.SelectedIndex = 1

            nombrec.Enabled = True
            apellidoc.Enabled = True
            ocupacionc.Enabled = True
            telefonoc.Enabled = True
            curpc.Enabled = True

            nombre.Enabled = False
            apellido.Enabled = False
            ocupacion.Enabled = False
            telefono.Enabled = False
            domicilio.Enabled = False
            curpAlumno.Enabled = False
            carrera.Enabled = False
            instituto.Enabled = False
            botonITSA.Checked = False
            botonITSA.Enabled = False

        End If
    End Sub

    'Alterna visibilidad de campos entre alumno regular y alumno ITSA
    Private Sub botonITSA_CheckedChanged_1(sender As Object, e As EventArgs) Handles botonITSA.CheckedChanged
        If botonITSA.Checked = True Then
            instituto.Enabled = False
            matricula.Enabled = True
            grado.Enabled = True

        Else
            instituto.Enabled = True
            matricula.Enabled = False
            grado.Enabled = False
        End If
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
#End Region

#Region "Botónes"
    'Botón para agregar el proyecto y moverse al modulo 2
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        validarProyectoForm()
    End Sub

    'Guarda al emprendedor en la lista de participantes
    Private Sub btn_agregar_participante_Click(sender As Object, e As EventArgs) Handles btn_agregar_participante.Click
        If nombreCompleto.Text = "" Then
            MsgBox("No se ha identificado un usuario", 16, "Llenar Campos")
            participante_grupo.Focus()


        ElseIf listaPart.Items.Count >= 5 Then
            MsgBox("Ya no se aceptan mas integrantes, solo se permiten 5 integrantes ", 16, "Error")

        ElseIf listaPart.Items.Count = 0 Then
            listaCurp(0) = participante_grupo.Text
            cont = 1
            listaPart.Items.Add(nombreCompleto.Text)
            nombreCompleto.Text = ""
            participante_grupo.Text = ""

        Else

            If listaPart.FindString(nombreCompleto.Text) = -1 Then
                listaCurp(cont) = participante_grupo.Text
                cont += 1
                listaPart.Items.Add(nombreCompleto.Text)
                nombreCompleto.Text = ""
                participante_grupo.Text = ""

            Else
                MsgBox("El usuario ya esta registrado en el proyecto", 16, "Reintentar")
                participante_grupo.Focus()
            End If

        End If

    End Sub

    'Registra emprendedores
    Private Sub btn_agregar_emp_Click(sender As Object, e As EventArgs) Handles btn_agregar_emp.Click
        validarEmprendedorForm()
    End Sub

    'Efecto de apertura de menu
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If PanelMenu.Width = 220 Then
            timeOcultarMenu.Enabled = True
        ElseIf PanelMenu.Width = 60 Then
            MostrarMenu.Enabled = True
        End If
    End Sub

    Private Sub cerrar_Click(sender As Object, e As EventArgs) Handles cerrar.Click
        If (MsgBox("¿Desea salir del programa?", 33, "Salir") = 1) Then
            Me.Close()
            End
        End If
    End Sub

    Private Sub minimizar_Click(sender As Object, e As EventArgs) Handles minimizar.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub diagnostico_btn_Click(sender As Object, e As EventArgs) Handles diagnostico_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(0)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub mecadotecnia_btn_Click(sender As Object, e As EventArgs) Handles mecadotecnia_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(1)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub administracion_btn_Click(sender As Object, e As EventArgs) Handles administracion_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(2)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub legalyfiscal_btn_Click(sender As Object, e As EventArgs) Handles legalyfiscal_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(3)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub produccion_btn_Click(sender As Object, e As EventArgs) Handles produccion_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(4)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub finanzas_btn_Click(sender As Object, e As EventArgs) Handles finanzas_btn.Click
        modulos.TabControl1.SelectedTab = modulos.TabControl1.TabPages(5)
        modulos.Show()
        Me.Hide()
    End Sub

    Private Sub reporte_btn_Click(sender As Object, e As EventArgs) Handles reporte_btn.Click
        Edicion.Show()
        Me.Hide()
    End Sub
#End Region

#Region "Temporizadores"
    Private Sub timeOcultarMenu_Tick(sender As Object, e As EventArgs) Handles timeOcultarMenu.Tick
        If PanelMenu.Width <= 60 Then
            Me.timeOcultarMenu.Enabled = False
        Else
            Me.PanelMenu.Width = PanelMenu.Width - 20
        End If
    End Sub

    Private Sub MostrarMenu_Tick(sender As Object, e As EventArgs) Handles MostrarMenu.Tick
        If PanelMenu.Width >= 220 Then
            Me.MostrarMenu.Enabled = False
        Else
            Me.PanelMenu.Width = PanelMenu.Width + 20
        End If
    End Sub
#End Region

#Region "Métodos y Funciones"
    Sub validarEmprendedorForm()
        'Validaciones
        If botonAlumno.Checked = True Then 'Si esta marcado alumno
            If nombre.Text = "" Then
                MsgBox("Llene el campo de su nombre", 16, "Llenar campo")
                nombre.Focus()

            ElseIf apellido.Text = "" Then
                MsgBox("Escriba su apellido", 16, "Llenar campo")
                apellido.Focus()


            ElseIf ocupacion.Text = "" Then
                MsgBox("Escriba su ocupacion", 16, "Llenar campo")
                ocupacion.Focus()

            ElseIf telefono.Text = "" Then
                MsgBox("Escriba su telefono", 16, "Llenar campo")
                telefono.Focus()

            ElseIf domicilio.Text = "" Then
                MsgBox("Escriba su domicilio", 16, "Llenar campo")
                domicilio.Focus()

            ElseIf Len(curpAlumno.Text) <> 18 Then
                MsgBox("Favor de completar la curp", 16, "Llenar campo")
                curpAlumno.Focus()

            ElseIf carrera.Text = "" Then
                MsgBox("Escriba su carrera", 16, "Llenar campo")
                carrera.Focus()

            ElseIf instituto.Text = "" Then
                MsgBox("Escriba su instituto", 16, "Llenar campo")
                instituto.Focus()

            Else 'Si todo esta validado, procede.
                If botonITSA.Checked = True Then 'Pregunta si es alumno ITSA para llenar campos matricula y grado
                    If matricula.Text = "" Then
                        MsgBox("Escriba su matricula", 16, "Llenar campo")
                        matricula.Focus()

                    ElseIf grado.Text = "" Then
                        MsgBox("Escriba su grado", 16, "Llenar campo")
                        grado.Focus()

                    Else 'Si todo esta validado, registra al Alumno ITSA

                        i = 1
                        Do 'Validacion para identificar espacios en la curp
                            If Mid(curpAlumno.Text, i, 1) = " " Then
                                MsgBox("La CURP no puede contener espacios.", 16, "Error")
                                Exit Sub
                            End If
                            i += 1
                        Loop Until (i = 18)


                        comando = New OleDb.OleDbCommand("SELECT COUNT(*) FROM emprendedor WHERE curp = @curp", conexion)
                        comando.Parameters.AddWithValue("@curp", curpAlumno.Text)
                        If Convert.ToInt32(comando.ExecuteScalar()) <> 0 Then 'Consulta si no existe la CURP en la BD
                            MsgBox("La curp de este usuario ya esta registrado.", 16, "Error")
                            curpAlumno.Focus()
                        Else
                            'alumno itsa
                            IdEmprendedor = IDEmp() 'genera ID aleatorio
                            comando = New OleDb.OleDbCommand("INSERT INTO emprendedor VALUES(@id, @nombre, @apellido, @matricula, @carrera, @grado, @telefono, @ocupacion, @domicilio, @institucion, @curp)", conexion)
                            comando.Parameters.AddWithValue("@id", IdEmprendedor)
                            comando.Parameters.AddWithValue("@nombre", nombre.Text)
                            comando.Parameters.AddWithValue("@apellido", apellido.Text)
                            comando.Parameters.AddWithValue("@matricula", matricula.Text)
                            comando.Parameters.AddWithValue("@carrera", carrera.Text)
                            comando.Parameters.AddWithValue("@grado", grado.Text)
                            comando.Parameters.AddWithValue("@telefono", telefono.Text)
                            comando.Parameters.AddWithValue("@ocupacion", ocupacion.Text)
                            comando.Parameters.AddWithValue("@domicilio", domicilio.Text)
                            comando.Parameters.AddWithValue("@institucion", "ITSA")
                            comando.Parameters.AddWithValue("@curp", curpAlumno.Text.ToUpper)
                            comando.ExecuteNonQuery() 'Crea registro -> emprendedor (Alumno ITSA)
                            validacion = True
                            limpiarBotones()

                        End If

                    End If
                Else 'Si no esta checado alumno ITSA, es alumno regular

                    i = 1
                    Do 'Validacion para identificar espacios en la curp
                        If Mid(curpAlumno.Text, i, 1) = " " Then
                            MsgBox("La CURP no puede contener espacios.", 16, "Error")
                            Exit Sub
                        End If
                        i += 1
                    Loop Until (i = 18)

                    comando = New OleDb.OleDbCommand("SELECT COUNT(*) FROM emprendedor WHERE curp = @curp", conexion)
                    comando.Parameters.AddWithValue("@curp", curpAlumno.Text)
                    If Convert.ToInt32(comando.ExecuteScalar()) <> 0 Then 'Consulta si no existe la CURP en la BD
                        MsgBox("La curp de este usuario ya esta registrado.", 16, "Error")
                        curpAlumno.Focus()
                    Else
                        'alumnos regulares de otras instituciones
                        IdEmprendedor = IDEmp() 'genera ID aleatorio
                        comando = New OleDb.OleDbCommand("INSERT INTO emprendedor VALUES(@id, @nombre, @apellido, @matricula, @carrera, @grado, @telefono, @ocupacion, @domicilio, @institucion, @curp)", conexion)
                        comando.Parameters.AddWithValue("@id", IdEmprendedor)
                        comando.Parameters.AddWithValue("@nombre", nombre.Text)
                        comando.Parameters.AddWithValue("@apellido", apellido.Text)
                        comando.Parameters.AddWithValue("@matricula", "") 'Se omite
                        comando.Parameters.AddWithValue("@carrera", carrera.Text)
                        comando.Parameters.AddWithValue("@grado", "") ''
                        comando.Parameters.AddWithValue("@telefono", telefono.Text)
                        comando.Parameters.AddWithValue("@ocupacion", ocupacion.Text)
                        comando.Parameters.AddWithValue("@domicilio", domicilio.Text)
                        comando.Parameters.AddWithValue("@institucion", instituto.Text)
                        comando.Parameters.AddWithValue("@curp", curpAlumno.Text.ToUpper)
                        comando.ExecuteNonQuery() 'Crea registro -> emprendedor (Alumno regular)
                        validacion = True
                        limpiarBotones()
                    End If


                End If
            End If

            'Si aun no se ha validado y se encuentra en otra pestaña, lo regresa
            If TabControl1.SelectedIndex = 1 And validacion = False Then
                TabControl1.SelectedIndex = 0
            Else 'Inicializa validación para nuevo registro
                validacion = False
            End If

        ElseIf botonAlumno.Checked = False Then 'Si no es alumno, es ciudadano
            If nombrec.Text = "" Then
                MsgBox("Llene el campo de su nombre", 16, "Llenar campo")
                nombrec.Focus()

            ElseIf apellidoc.Text = "" Then
                MsgBox("Escriba su apellido", 16, "Llenar campo")
                apellidoc.Focus()

            ElseIf ocupacionc.Text = "" Then
                MsgBox("Escriba su ocupacion", 16, "Llenar campo")
                ocupacionc.Focus()

            ElseIf telefonoc.Text = "" Then
                MsgBox("Escriba su telefono", 16, "Llenar campo")
                telefonoc.Focus()

            ElseIf Len(curpc.Text) <> 18 Then
                MsgBox("Favor de llenar correctamente su curp", 16, "Llenar campo")
                curpc.Focus()

            Else 'Si esta validado, procede.
                comando = New OleDb.OleDbCommand("SELECT COUNT(*) FROM emprendedor WHERE curp = @curp", conexion)
                comando.Parameters.AddWithValue("@curp", curpc.Text)
                If Convert.ToInt32(comando.ExecuteScalar()) <> 0 Then 'Consulta si no existe la CURP en la BD
                    MsgBox("La curp de este usuario ya esta registrado.", 16, "Error")
                    curpc.Focus()
                Else

                    i = 1
                    Do 'Validacion para identificar espacios en la curp
                        If Mid(curpc.Text, i, 1) = " " Then
                            MsgBox("La CURP no puede contener espacios.", 16, "Error")
                            Exit Sub
                        End If
                        i += 1
                    Loop Until (i = 18)

                    'ciudadano
                    IdEmprendedor = IDEmp() 'genera ID aleatorio
                    comando = New OleDb.OleDbCommand("INSERT INTO emprendedor VALUES(@id, @nombre, @apellido, @matricula, @carrera, @grado, @telefono, @ocupacion, @domicilio, @institucion, @curp)", conexion)
                    comando.Parameters.AddWithValue("@id", IdEmprendedor)
                    comando.Parameters.AddWithValue("@nombre", nombrec.Text)
                    comando.Parameters.AddWithValue("@apellido", apellidoc.Text)
                    comando.Parameters.AddWithValue("@matricula", "") 'Se omite
                    comando.Parameters.AddWithValue("@carrera", "") ''
                    comando.Parameters.AddWithValue("@grado", "") ''
                    comando.Parameters.AddWithValue("@telefono", telefonoc.Text)
                    comando.Parameters.AddWithValue("@ocupacion", ocupacionc.Text)
                    comando.Parameters.AddWithValue("@domicilio", "N/A") ''
                    comando.Parameters.AddWithValue("@institucion", "") ''
                    comando.Parameters.AddWithValue("@curp", curpc.Text.ToUpper)
                    comando.ExecuteNonQuery() 'Crea registro -> emprendedor (Ciudadano)
                    validacion = True
                    limpiarBotones()
                End If

            End If

            'Si aun no se ha validado y se encuentra en otra pestaña, lo regresa
            If TabControl1.SelectedIndex = 0 And validacion = False Then
                TabControl1.SelectedIndex = 1
            Else
                validacion = False 'Inicializa validación para nuevo registro
            End If
        End If

    End Sub

    Sub validarProyectoForm()
        'Validaciones
        If listaPart.Items.Count = 0 Then
            MsgBox("Agregar emprendedores al proyecto", 16, "Error")
            participante_grupo.Focus()
        ElseIf AsesorIncubadora.Text = "" Then
            MsgBox("Agregar el asesor de incubadora", 16, "Error")
            AsesorIncubadora.Focus()

        ElseIf asesor_grupo.Text = "" Then
            MsgBox("Agregar el asesor academico", 16, "Error")
            asesor_grupo.Focus()
        Else 'Agrega el proyecto si todo esta rellenado
            agregarProyecto()
        End If
    End Sub

    Sub agregarProyecto()
        IdProyecto = IDProy() 'Genera ID aleatorio

        comando = New OleDb.OleDbCommand("INSERT INTO proyecto VALUES(@IdProy, @asesorA, @asesorInc)", conexion)
        comando.Parameters.AddWithValue("@IdProy", IdProyecto)
        comando.Parameters.AddWithValue("@asesorA", asesor_grupo.Text)
        comando.Parameters.AddWithValue("@asesorInc", AsesorIncubadora.Text)
        comando.ExecuteNonQuery() 'Crea un registro -> proyecto

        i = 0
        Do 'Ciclo para asignar en la tabla EmpProy, el ID del proyecto para cada participante.
            comando = New OleDb.OleDbCommand("SELECT * FROM emprendedor WHERE curp = @curp", conexion)
            comando.Parameters.AddWithValue("@curp", listaCurp(i))
            rs = comando.ExecuteReader() 'Consulta la CURP para rescatar el ID del emprendedor
            If rs.Read Then
                IdEmprendedor = rs(0).ToString
            End If

            comando = New OleDb.OleDbCommand("INSERT INTO EmpProy VALUES (@idEmp, @idProy)", conexion)
            comando.Parameters.AddWithValue("@idEmp", IdEmprendedor)
            comando.Parameters.AddWithValue("@idProy", IdProyecto)
            comando.ExecuteNonQuery() 'Crea un registro -> EMpProy

            i += 1
        Loop Until (i = listaPart.Items.Count)

        idGlobal = IdProyecto 'Traspasa el ID del proyecto a otros formularios como IDGlobal (Para guardar Modulo 2-7)

        'Limpiar variables
        IdEmprendedor = ""
        IdProyecto = ""
        For i = 0 To i < 5
            listaCurp(i) = ""
        Next
        cont = 0

        'Se mueve al modulo 2
        modulos.Show()
        Me.Hide()
    End Sub

    Sub limpiarBotones()
        botonAlumno.Checked = False
        nombrec.Text = ""
        apellidoc.Text = ""
        ocupacionc.Text = ""
        telefonoc.Text = ""
        curpc.Text = ""

        nombre.Text = ""
        apellido.Text = ""
        ocupacion.Text = ""
        telefono.Text = ""
        domicilio.Text = ""
        curpAlumno.Text = ""
        carrera.Text = ""
        instituto.Text = ""
        matricula.Text = ""
        grado.Text = ""
        botonITSA.Checked = False
        botonITSA.Enabled = False
    End Sub

    Function IDProy()
        'genera id aleatorio PY
        Dim formato As String = "PY" + CStr(Math.Round(Rnd() * 99999998) + 1)
        'cuenta los registros que coincidan con el id generado
        comando = New OleDb.OleDbCommand("SELECT COUNT(*) FROM proyecto WHERE idProy = @id", conexion)
        comando.Parameters.AddWithValue("@id", formato)
        'consulta si se repite para despues determinar si generar uno nuevo o guardarlo en caso contrario
        If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
            Return formato
        Else
            IDProy()
        End If
    End Function

    Function IDEmp()
        'genera id aleatorio EM
        Dim formato As String = "EM" + CStr(Math.Round(Rnd() * 99999998) + 1)
        'cuenta los registros que coincidan con el id generado
        comando = New OleDb.OleDbCommand("SELECT COUNT(*) FROM emprendedor WHERE idEmp = @id", conexion)
        comando.Parameters.AddWithValue("@id", formato)
        'consulta si se repite para despues determinar si generar uno nuevo o guardarlo en caso contrario
        If Convert.ToInt32(comando.ExecuteScalar()) = 0 Then
            Return formato
        Else
            IDEmp()
        End If
    End Function
#End Region


End Class





















































''horas de estres: 10'''
