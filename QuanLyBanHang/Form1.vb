Imports System.Data.SqlClient

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim chuoiketnoi As String = "workstation id=qldienthoai.mssql.somee.com;packet size=4096;user id=phongtd02;pwd=Abc!@#123;data source=qldienthoai.mssql.somee.com;persist security info=False;initial catalog=qldienthoai"

        Dim KetNoi As SqlConnection = New SqlConnection(chuoiketnoi)
        Dim sqlAdapter As New SqlDataAdapter("select * from KhachHang  ", KetNoi)
        Dim tb As New DataTable

        Try
            KetNoi.Open()
            sqlAdapter.Fill(tb)
            DataGridView1.DataSource = tb


        Catch ex As Exception

        End Try
    End Sub
End Class