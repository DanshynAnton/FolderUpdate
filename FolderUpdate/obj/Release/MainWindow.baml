   M S B A M L   `   `   `  ���� G  CFolderUpdate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FolderUpdateFolderUpdate      FolderUpdate.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:FolderUpdate     -     � Title$  FolderUpdate��$	��600��$	��800�� � Icon$ resources\ico.png��.���    
cLightGrad#��[   � ColumnDefinitions �� $��40 ��� $
��auto �$��40���� $
��auto ��� $	��200 ��� $��1* �$	��150���� $��40 � � RowDefinitions �� $��70 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��1* �$	��100��$	��300���� $��60��$��1* ��� $��35 �.+��� -    ��Name$ tbBaseFolder��$��3��$��1��$��20��$	��Top=�$��Stretch=�� $��4��$��5�� �� $
��auto ��� $��1* �.+�(  "System.Windows.Controls.DatePicker  -     Name$ dpDefaultDate��$��0��$	��Top=�$��5, -5, 5, 0q�$��14(�$
��Left=�$��30���� -   $ tbDefaultTime��$��1��$	��Top=�$��
5, 0, 5, 0q�$
��Left=�$��20��$	��100���� $��0��$��0��$��6��$��FolderUpdate��$��Stretch=�$��Stretch=� �� TextAlignment$
 Left=� �� Padding$ 45, 10, 5, 5q�$��0, 0, 0, 15q�$��26(� 
 cWhite#��[   	cDarkBlue#��[ �� -    ��Name$ LBaseFolder��$��1��$��1��$��2��$%��Вихідний каталог�� 
 labels#��[ �� -   $ LSubFoldersDepth��$��2��$��3��$)��#Глибина сканування��#��[ �� -   $ LEmptyFolders��$��1��$��5��$��2��$;��5Дія над порожніми каталогами��#��[ �� -   	 ��Name$	 cbSubFoldersAnalise��$��1��$��2��$��3��$3��-Аналізувати підкаталоги���� -	   $	 cbSubFoldersChange��$��2��$��4��$��2��$K��EЗмінювати час створення підкаталогів��{��� 
 �� ElementName$
 cbSubFoldersAnalise�� �� Path$ 	IsChecked� �� Mode$ OneWay=��� -
    ��Name$ dbSubFoldersDepth��$��3��$��3��{��� $
 cbSubFoldersAnalise��$ 	IsChecked�$ OneWay=�.@��� -    ��Name$ cbiDepthUnlimited�� �� 
IsSelected . �� -   $ dbEmptyFolders��$��3��$��5��$��Stretch=�$	��Top=�.@��� -   $ cbiDoNotUse�� . �� -   $ 
cbiTimeUTC���� -   $ cbiTimeLocal��&   System.Windows.Controls.DataGrid  -     Name$ dgvLog��$��1��$��6��$��4��$
��Left=�$	��100��$��Stretch=�$	��450��$
��1000���� -    ��Name$ bBaseFolderSelect��$��4��$��1��$	��...��$
��Left=�$	��Top=�$��21��$��21���� $��1��$��7��$��3�� �� Orientation$ 
Horizontal=�$��Right=�$	��Top=�$��5, 10, 0, 5q�.+��� -   $ bStart��$
��auto��$��Start��$��70���� -   $ bSaveLog��$
��auto��$��Save log��{�.  �� -   $ bSaveConfig��$
��auto��$��Save config���� -   $ bLoadConfig��$
��auto��$��Load Config��� $��4��$��1��$��3�� ��  �� $
��auto ��� $��1* �.+��� -   $ tbLang��$��0��$��0��$��
0, 0, 5, 0q�$��Right=�$	��Top=�$
��auto��$	��LNG���� -   $ dbLang��$��0��$��1��$��5q�$��Right=�$	��Top=�$
��auto���� $��0��$��8��$��6��  cStrongBlue#��[ 