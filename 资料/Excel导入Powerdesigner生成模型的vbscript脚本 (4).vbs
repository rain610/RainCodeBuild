'******************************************************************************
'开始
Option Explicit
Dim tab_name,tab_code,tab_comment
Dim b_r, e_r, s_r, j, m, n
Dim mdl ' the current model
dim count
Dim HaveExcel
Dim RQ
Dim file_name,WScript
Set mdl = ActiveModel
If (mdl Is Nothing) Then
   MsgBox "There is no Active Model"
End If
RQ = vbYes 'MsgBox("Is Excel Installed on your machine ?", vbYesNo + vbInformation, "Confirmation")
If RQ = vbYes Then
   HaveExcel = True
   ' Open & Create Excel Document
Else
   HaveExcel = False
End If
file_name = selectFile() 
if file_name <> "" then
   Dim x1  '
   Set x1 = CreateObject("Excel.Application")
   x1.Workbooks.Open file_name    '指定excel文档路径
   x1.Workbooks(1).Worksheets("表结构").Activate   '指定要打开的sheet名称
j = 1
do while n < 11
if x1.Workbooks(1).Worksheets("表结构").cells(j,1).value <> "" then
call a(x1, mdl, j, getRow(x1, j))
'msgbox j & "--" & getRow(x1, j)
count = count + 1
j = getRow(x1, j)
n = 0
end if
j = j + 1
n = n + 1
loop
'MsgBox "生成数据表结构共计 " + CStr(count), vbOK + vbInformation, "表 导入完毕!"
MsgBox "生成数据表结构共计 " & CStr(count) & " 表导入完毕!"
x1.Workbooks(1).close
x1.quit
else
msgbox "没有选择文件!"
end if

sub a(x1, mdl, r_0,r_9)
dim rwIndex   
dim tableName
dim colname
dim table
dim col

'tab_name = ucase(x1.Workbooks(1).Worksheets("表结构").cells(r_0,1).value)    '指定表名，如果在Excel文档里有，也可以 .Cells(rwIndex, 3).Value 这样指定
'tab_code = ucase(x1.Workbooks(1).Worksheets("表结构").cells(r_0,2).value)  '指定表名
'tab_comment = ucase(x1.Workbooks(1).Worksheets("表结构").cells(r_0,3).value)
tab_name = x1.Workbooks(1).Worksheets("表结构").cells(r_0,1).value    '指定表名，如果在Excel文档里有，也可以 .Cells(rwIndex, 3).Value 这样指定
tab_code = x1.Workbooks(1).Worksheets("表结构").cells(r_0,2).value  '指定表名
tab_comment = x1.Workbooks(1).Worksheets("表结构").cells(r_0,3).value

on error Resume Next
set table = mdl.Tables.CreateNew '创建一个表实体
table.Name = tab_name
table.Code = tab_code
table.Comment = tab_comment

For rwIndex = r_0 + 2 To r_9   '指定要遍历的Excel行标  由于第1行是表头，从第2行开始
        With x1.Workbooks(1).Worksheets("表结构")
            If .Cells(rwIndex, 2).Value = "" Then
               Exit For
            End If
               set col = table.Columns.CreateNew   '创建一列/字段
               'MsgBox .Cells(rwIndex, 1).Value, vbOK + vbInformation, "列"
               If .Cells(rwIndex, 1).Value = "" Then
                  col.Name = .Cells(rwIndex, 2).Value 'ucase(.Cells(rwIndex, 2).Value)   '指定列名
               Else 
                  col.Name = .Cells(rwIndex, 1).Value 'ucase(.Cells(rwIndex, 1).Value)
               End If
               'MsgBox col.Name, vbOK + vbInformation, "列"
               col.Code = .Cells(rwIndex, 2).Value 'ucase(.Cells(rwIndex, 2).Value)   '指定列名
               
               col.DataType = .Cells(rwIndex, 3).Value 'ucase(.Cells(rwIndex, 3).Value)   '指定列数据类型
               
               col.Comment = .Cells(rwIndex, 4).Value 'ucase(.Cells(rwIndex, 4).Value)  '指定列说明
               
               col.defaultvalue = ucase(.Cells(rwIndex,8).value)

               If ucase(.Cells(rwIndex, 5).Value) = "Y" Then
                   col.Primary = true    '指定主键
               End If 
               If ucase(.Cells(rwIndex,6).value) = "Y" then
                   col.Mandatory = true                
               End If
               If ucase(.Cells(rwIndex,7).value) = "Y" then
                   col.Identity = true                
               End If
               
        End With
Next
End sub

Function getRow(x1, s_r)
dim i, k
k = s_r
do while x1.Workbooks(1).Worksheets("表结构").cells(k,1).value <> ""
k = k + 1
if x1.Workbooks(1).Worksheets("表结构").cells(k,1).value = "" then
getRow = k - 1
exit function
end if
loop
End Function
Function SelectFile()
    Dim shell : Set shell = CreateObject("WScript.Shell")
    Dim fso : Set fso = CreateObject("Scripting.FileSystemObject")
    Dim tempFolder : Set tempFolder = fso.GetSpecialFolder(2)
    Dim tempName : tempName = fso.GetTempName()
    Dim tempFile : Set tempFile = tempFolder.CreateTextFile(tempName & ".hta")
    tempFile.Write _
    "<html>" & _
    "<head>" & _
    "<title>Browse</title>" & _
    "</head>" & _
    "<body>" & _
    "<input type='file' id='f' />" & _
    "<script type='text/javascript'>" & _
    "var f = document.getElementById('f');" & _
    "f.click();" & _
    "var shell = new ActiveXObject('WScript.Shell');" & _
    "shell.RegWrite('HKEY_CURRENT_USER\\Volatile Environment\\MsgResp', f.value);" & _
    "window.close();" & _
    "</script>" & _
    "</body>" & _
    "</html>"
    tempFile.Close
    shell.Run tempFolder & "\" & tempName & ".hta", 0, True
    SelectFile = shell.RegRead("HKEY_CURRENT_USER\Volatile Environment\MsgResp")
    shell.RegDelete "HKEY_CURRENT_USER\Volatile Environment\MsgResp"
End Function