   M S B A M L   `   `   `  ����C  ?RestFlow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:RestMenef	RestMenef  5   	       RestMenef.Admin_Window   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:RestMenef     5   	     � Title$  Admin_Window��5   	   $	��450��6   $	��800��6+   .��5	      � .+�5
   
   �� .@�5      �� $)��#Таблица работников��6   .��5      �� .+�5      � $
��Left=�6   $	��383��67   $��10q�6D   $	��775��6P   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ DataGrid_WorkersInfo��5   6   $��	0,0,387,0q�6#      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   "   ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 58* 65      Header$ ФИО��6A   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6N   �� *Dish+6i     5   "   $	 27* 65   $ Должность��6A    6T   �� *
Цена+6o     5   "   $	 36* 65   $ Дата рождения��6A    6X   �� *Кол-во+6u     5   "   $	 22* 65   $ Зарплата��6A    6S   �� *Sum+6m   5      � 5      $��
406,0,0,61q�6   
�������.+�5      �� $��Имя��6$   $
��Left=�62   $��5,5,0,0q�6M   $	��Top=�6^   $��14(�6v   $��40��6�   �� 5      $��Фамилия��6$   $
��Left=�66   $��5,40,0,0q�6Q   $	��Top=�6c   $��14(�6{   $��73��6�   �� 5      $��Отчество��6$   $
��Left=�67   $��5,75,0,0q�6R   $	��Top=�6d   $��14(�6|   $��74��6�   �� 5      $��Пол��6$   $
��Left=�62   $��	5,110,0,0q�6M   $	��Top=�6`   $��14(�6x   $��38��6�   �� 5      $��Дата рожденья��6$   $
��Left=�6<   $��	5,145,0,0q�6W   $	��Top=�6j   $��14(�6�   $	��116��6�   �� 5      $��
Почта��6$   $
��Left=�64   $��	5,180,0,0q�6O   $	��Top=�6b   $��14(�6z   $��52��6�   �� 5      $��Телефон��6$   $
��Left=�66   $��	5,215,0,0q�6Q   $	��Top=�6d   $��14(�6|   $��70��6�   �� 5      $��Должность��6$   $
��Left=�68   $��	5,250,0,0q�6S   $	��Top=�6f   $��14(�6~   $��88��6�   �� 5      $��Зарплата��6$   $
��Left=�67   $��	5,285,0,0q�6R   $	��Top=�6e   $��14(�6}   $��73��6�   �� 5       -   5         ��Name$ TextBox_WorkerName��5    &   $
��Left=�6B   $��	113,8,0,0q�6]    �� TextWrapping$
 Wrap=�6p   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5!      -   5        $ TextBox_WorkerSurename��5!   &   $
��Left=�6F   $��
113,43,0,0q�6a   $
 Wrap=�6u   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5"      -   5        $ TextBox_WorkerMiddlename��5"   &   $
��Left=�6H   $��
113,78,0,0q�6c   $
 Wrap=�6w   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5#      -   5        $ TextBox_WorkerEmail��5#   &   $
��Left=�6C   $��113,183,0,0q�6^   $
 Wrap=�6s   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5$      -   5        $# TextBox_WorkerTelephoneNumber��5$   &   $
��Left=�6M   $��113,218,0,0q�6h   $
 Wrap=�6}   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5%      -   5        $ TextBox_WorkerPost��5%   &   $
��Left=�6B   $��113,253,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $	��246��6�   $��15(�6�   �� 5&      -   5        $ TextBox_WorkerSalary��5&   &   $
��Left=�6D   $��113,288,0,0q�6_   $
 Wrap=�6t   $	��Top=�6�   $	��246��6�   $��15(�6�   	� 5'      -	   5         	�Name$ RadioButton_Female��5'   *   $��Женщина��6F   $
��Left=�6X   $��113,116,0,0q�6s   $	��Top=�6�   $��23��6�   	� 5(      -
   5        $ RadioButton_mMle��5(   *   $��Мужчина��6D   $
��Left=�6V   $��200,116,0,0q�6q   $	��Top=�6�   $��23��6�   (  "System.Windows.Controls.DatePicker  5)      -   5        	  Name$	 DatePicker_Birthday��5)   )   $
��Left=�6F   $��	113,0,0,0q�6a   $��Center=�6t   $	��123��6�   5*      �� 5+      -   5        
 ��Name$
 Button_SaveChanges��5+   !   $+��%Сохранить изменения��6=   $
��Left=�6[   $��406,327,0,21q�6v   $	��170��6�   $��16(�6�   �� 5,      -   5        $
 Button_Dismiss��5,   !   $��Уволить��69   $
��Left=�6K   $��590,327,0,21q�6f   $	��170��6|   $��16(�6�   5-      5/      50      �� 51      $)��#Добавить работника��6   .��52      � $
��Left=�6   $	��796��62   $	��415��6>   .+�53      �� $��Новый аккаунт��6   $
��Left=�64   $��	20,20,0,0q�6O   $	��Top=�6b   $��20(�6z   � 54      $��	0,30,0,20q�6   .+�55      �� $��Имя��6    $
��Left=�6.   $��	35,63,0,0q�6I   $	��Top=�6\   $��15(�6t   $��30��6�   $��40��6�   �� 56      $��Фамилия��6    $
��Left=�62   $��
232,63,0,0q�6M   $	��Top=�6a   $��15(�6y   $��30��6�   $��75��6�   �� 57      $��Отчество��6    $
��Left=�63   $��
505,63,0,0q�6N   $	��Top=�6b   $��15(�6z   $��30��6�   $��74��6�   �� 58      $��Пол��6    $
��Left=�6.   $��
35,103,0,0q�6I   $	��Top=�6]   $��15(�6u   $��30��6�   $��38��6�   �� 59      $��Дата рождения��6    $
��Left=�68   $��259,103,0,0q�6S   $	��Top=�6h   $��15(�6�   $��30��6�   $	��120��6�   �� 5:      $��
Почта��6    $
��Left=�60   $��
34,149,0,0q�6K   $	��Top=�6_   $��15(�6w   $��30��6�   $��52��6�   �� 5;      $��Телефон��6    $
��Left=�62   $��345,149,0,0q�6M   $	��Top=�6b   $��15(�6z   $��30��6�   $��71��6�   �� 5<      $��Должность��6    $
��Left=�64   $��
36,188,0,0q�6O   $	��Top=�6c   $��15(�6{   $��30��6�   $��88��6�   �� 5=      $��Зарплата��6    $
��Left=�63   $��345,188,0,0q�6N   $	��Top=�6c   $��15(�6{   $��30��6�   $��71��6�   �� 5>      -   5        $ TextBox_NewWorkerName��5>   "   $
��Left=�6A   $��	80,67,0,0q�6\   $
 Wrap=�6o   $	��Top=�6�   $	��147��6�   $��15(�6�   $��22��6�   �� 5?      -   5        $ TextBox_NewWorkerSurename��5?   "   $��Center=�6E   $��0,67,0,0q�6b   $
 Wrap=�6t   $	��Top=�6�   $	��194��6�   $��15(�6�   $��22��6�   �� 5@      -   5        $! TextBox_NewWorkerMiddlename��5@   "   $
��Left=�6G   $��
584,66,0,0q�6b   $
 Wrap=�6v   $	��Top=�6�   $	��179��6�   $��15(�6�   $��23��6�   �� 5A      -   5        $ TextBox_NewWorkerEmail��5A   "   $
��Left=�6B   $��129,152,0,0q�6]   $
 Wrap=�6r   $	��Top=�6�   $	��211��6�   $��15(�6�   $��23��6�   �� 5B      -   5        $&  TextBox_NewWorkerTelephoneNumber��5B   "   $
��Left=�6L   $��421,152,0,0q�6g   $
 Wrap=�6|   $	��Top=�6�   $	��202��6�   $��15(�6�   $��23��6�   �� 5C      -   5        $ TextBox_NewWorkerPost��5C   "   $
��Left=�6A   $��129,191,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $	��211��6�   $��15(�6�   $��23��6�   �� 5D      -   5        $ TextBox_NewWorkerSalary��5D   "   $
��Left=�6C   $��421,192,0,0q�6^   $
 Wrap=�6s   $	��Top=�6�   $	��202��6�   $��15(�6�   $��22��6�     5E      -   5        $"	 DatePicker_NewWorkerBirthday��5E   %   $
��Left=�6K   $��384,107,0,0q�6f   $	��Top=�6{   $��22��6�   $	��111��6�   	� 5F      -   5        $! RadioButton_NewWorkerFemale��5F   &   $��Женщина��6K   $
��Left=�6]   $��
81,112,0,0q�6x   $	��Top=�6�   $��23��6�   	� 5G      -   5        $ RadioButton_NewWorkerMale��5G   &   $��Мужчина��6I   $
��Left=�6[   $��168,112,0,0q�6v   $	��Top=�6�   $��23��6�   �� 5H      -   5        $
 Button_AddWorker��5H   !   $��Добавить��6;   $��674,313,10,10q�6N   $��16(�6e   6v   5I      5J      5K      �� 5L      $��Мой аккаунт��6   $
��Left=�6+   .��5M      � $��	70,0,56,0q�6   .+�5N      �� $��Мой аккаунта��6   $
��Left=�63   $��	20,20,0,0q�6N   $	��Top=�6a   $��20(�6y   �� 5O      $��
Логин��6   $
��Left=�6,   $��5,60,0,0q�6G   $	��Top=�6Y   $��15(�6q   $��30��6   $��94��6�   �� 5P      $��Пароль��6   $
��Left=�6-   $��
324,60,0,0q�6H   $	��Top=�6\   $��15(�6t   $��30��6�   $��75��6�   �� 5Q      -   5        $ TextBox_InfoLogin��5Q      $
��Left=�69   $��
103,66,0,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $	��205��6�   $��15(�6�   $��23��6�   �� 5R      -   5        $ TextBox_InfoPassword��5R      $
��Left=�6<   $��
404,66,0,0q�6W   $
 Wrap=�6k   $	��Top=�6   $	��209��6�   $��15(�6�   $��23��6�   �� 5S      -   5         ��Name$ Label_AllInfo��5S   *   $��ФИО��6   $��5,90,0,0q�6A   6U   5T      5U      5V      5W      5X      