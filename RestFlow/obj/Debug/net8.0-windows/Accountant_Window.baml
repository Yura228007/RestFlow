   M S B A M L   `   `   `  ����C  ?RestFlow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:RestMenef	RestMenef  5   	   !    RestMenef.Accountant_Window   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:RestMenef     5   	     � Title$  Accountant_Window��5   	   $	��450��6#   $	��800��60   .��5	      �� .@�5
   
   �� $��Таблицы��6   .��5      � .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGrid_Acountant��5      $��10,10,10,111q�67      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 15* 6-      Header$ Название��69   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *Dish+6f     5      $	 25* 6-   $ Цена��69    6G   �� *
Цена+6b     5      $	 25* 6-   $ Дата��69    6G   �� *
Дата+6b     5      $	 25* 6-   $ Расход��69    6I   �� *Расход+6f   5      �� 5      $"��Итоговый доход:��6   $��10,297,682,83q�62   6K   �� 5      -   5         ��Name$ Label_TotalCost��5      $�� ��61   $��117,297,402,83q�6<   6V   �� 5      $$��Итоговый расход:��6   $��397,297,288,83q�63   6M   �� 5      -   5        $ TotalExpanses��5      $�� ��6/   $��506,297,10,83q�6:   6S   �� 5      $��Начало:��6   $��10,328,733,47q�6*   6C   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ DatePicker_StartData��5      $
��Left=�6;   $��
66,328,0,0q�6V   $	��Top=�6j   $��31��6�   �� 5      $��Конец:��6   $��397,328,349,47q�6)   6C     5      -   5        $ DatePicker_FinishData��5      $
��Left=�6<   $��450,328,0,0q�6W   $	��Top=�6l   $��31��6�   �� 5      -   5         ��Name$ Button_GetReport��5      $!��Получить отчёт��63   $��Center=�6L   $��	0,364,0,0q�6i   $	��Top=�6|   	 J� RenderTransformOrigin$	 0.025,-0.098&�6�   $��32��6�   $	��774��6�   $��15(�6�   5      5       �� 5!   
   $��Мой аккаунт��6   $
��Left=�6'   .��5"      � $��	70,0,56,0q�6   .+�5#      �� $��Мой аккаунта��6   $
��Left=�6/   $��	20,20,0,0q�6J   $	��Top=�6]   $��20(�6u   �� 5$      $��
Логин��6   $
��Left=�6(   $��5,60,0,0q�6C   $	��Top=�6U   $��15(�6m   $��30��6{   $��94��6�   �� 5%      $��Пароль��6   $
��Left=�6)   $��
324,60,0,0q�6D   $	��Top=�6X   $��15(�6p   $��30��6~   $��75��6�   �� 5&      -   5        
 ��Name$
 TextBox_InfoLogin��5&      $
��Left=�65   $��
103,66,0,0q�6P    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��205��6�   $��15(�6�   $��23��6�   �� 5'      -   5        $
 TextBox_InfoPassword��5'      $
��Left=�68   $��
404,66,0,0q�6S   $
 Wrap=�6g   $	��Top=�6{   $	��209��6�   $��15(�6�   $��23��6�   �� 5(      -	   5        $ Label_AllInfo��5(   &   $��ФИО��6   $��5,90,0,0q�6=   6Q   5)      5*      5+      5-      