
Imports System.Data.SqlClient
Imports System.Data.DataTable


Public Class QLSanPham
    Dim tb As New DataTable
    Dim connectstr As String = "workstation id=ps00010.mssql.somee.com;packet size=4096;user id=ps0001;pwd=Abc!@#123;data source=ps00010.mssql.somee.com;persist security info=False;initial catalog=ps00010"

    Public Sub LoadData()
        Dim KetNoi As New SqlConnection(connectstr)
        Dim sqlAdapter As New SqlDataAdapter("select * from NhanVien", KetNoi)

        Try
            sqlAdapter.Fill(tb)

        Catch ex As Exception

        End Try
        DataGridView1.DataSource = tb
        KetNoi.Close()
    End Sub

    Private Sub QLSanPham_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'tạo đối tượng kết nối tới Data Trên Somee.com thông qua chuỗi kết nối connectstr
        Dim KetNoi As New SqlConnection(connectstr)
        'Tạo đối tượng chạy câu truy vấn 
        Dim sqlAdapter As New SqlDataAdapter("select * from NhanVien", KetNoi)

        Try
            KetNoi.Open()
            'Đổ dữ liệu trên Table vào Datatable trên máy
            sqlAdapter.Fill(tb)

        Catch ex As Exception

        End Try
        'Hiển thị dữ liệu Từ Datatable ra giao diện thông qua Datagridview
        DataGridView1.DataSource = tb
        KetNoi.Close()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Khi click vào 1 cell bất kỳ, lấy giá trị tại dòng đó đưa lên các textbox tương ứng
        Dim index As Integer = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, index).Value
        TextBox2.Text = DataGridView1.Item(1, index).Value
        TextBox3.Text = DataGridView1.Item(2, index).Value

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Kết nối tới Database online thông qua chuỗi kết nối connectstr
        Dim KetNoi As New SqlConnection(connectstr)
        'Mở kết nối
        KetNoi.Open()

        Dim stradd As String = "insert into NhanVien Values (@MaNhanVien,@Password,@TenNhanVien)"
        Dim com As New SqlCommand(stradd, KetNoi)
        Try
            com.Parameters.AddWithValue("@MaNhanVien", TextBox1.Text)
            com.Parameters.AddWithValue("@Password", TextBox2.Text)
            com.Parameters.AddWithValue("@TenNhanVien", TextBox3.Text)
            'Sẽ thực thi câu lệnh insert dữ liệu vào Database Online
            com.ExecuteNonQuery()
            'Đóng kết nối
            KetNoi.Close()
        Catch ex As Exception
            MessageBox.Show("Ket noi khong thanh cong")
        End Try
        tb.Clear()
        DataGridView1.DataSource = tb
        DataGridView1.DataSource = Nothing
        LoadData()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Kết nối tới Database online thông qua chuỗi kết nối connectstr
        Dim KetNoi As New SqlConnection(connectstr)
        'Mở kết nối
        KetNoi.Open()
        'Câu truy vấn để sửa dữ liệu theo mã
        Dim stradd As String = "UPDATE NhanVien SET MaNhanVien = @TenSanPham, Password = @DonGia, SoLuong = @SoLuong, ChiTietSP = @ChiTiet WHERE MaSanPham = @MaSanPham"
        Dim com As New SqlCommand(stradd, KetNoi)
        Try
            'Thêm dữ liệu vào câu truy vấn
            com.Parameters.AddWithValue("@MaSanPham", TextBox1.Text)
            com.Parameters.AddWithValue("@TenSanPham", TextBox2.Text)
            com.Parameters.AddWithValue("@DonGia", TextBox3.Text)
            com.Parameters.AddWithValue("@SoLuong", TextBox4.Text)
            com.Parameters.AddWithValue("@ChiTiet", TextBox5.Text)
            'Thực thi câu truy vấn và sửa dữ liệu trong Database
            com.ExecuteNonQuery()
            'Đóng kết nối
            KetNoi.Close()
        Catch ex As Exception
            MessageBox.Show("Ket noi khong thanh cong")
        End Try
        'Load lại sản phẩm 
        tb.Clear()
        DataGridView1.DataSource = tb
        DataGridView1.DataSource = Nothing
        LoadData()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim KetNoi As New SqlConnection(connectstr)
        KetNoi.Open()
        Dim stradd As String = "Delete from SanPham WHERE MaSanPham = @MaSanPham"
        Dim com As New SqlCommand(stradd, KetNoi)
        Try
            com.Parameters.AddWithValue("@MaSanPham", TextBox1.Text)
            com.ExecuteNonQuery()
            KetNoi.Close()
        Catch ex As Exception
            MessageBox.Show("Ket noi khong thanh cong")
        End Try
        tb.Clear()
        DataGridView1.DataSource = tb
        DataGridView1.DataSource = Nothing
        LoadData()
    End Sub
End Class