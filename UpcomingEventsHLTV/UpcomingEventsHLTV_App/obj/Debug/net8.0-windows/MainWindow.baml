   M S B A M L   `   `   `  ����Q  MUpcomingEventsHLTV_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:UpcomingEventsHLTV_AppUpcomingEventsHLTV_App  5   	   '    !UpcomingEventsHLTV_App.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:UpcomingEventsHLTV_App     5   	   -   5        
������   � WindowStartupLocation$  CenterScreen=�5   "    � Title$ Upcoming events��5	   	   $	��650��6!   $
��1020��6.    � 	Resources 5
      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell��%   '        '  J     �� 5   
   "  6   .�5      ��  � ��
 ���� 6+   5      �� 5   
   "  6   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   ���#14212b.+�5      ��  �� Orientation$ Vertical=�6   $��Bottom=�60   .+�5      �� -   5         ��Name$ WordBtn��5   ;      BtnStyle#��[  6   .��5      �� $ 
Horizontal=�6!   .+�5      �� $��Images/File.png��6      	IconStyle#��[ 69   �� 5       	Word file6/   5!      	 � ToolTip	 5"      _� 5#        ToolTipStyle#��[ 6"   .��6H   )'Get MS WORD file with info about events6q   5%      �� 5'      -   5        $ ExcelBtn��5'   ;   #��[  6   .��5(      �� $ 
Horizontal=�6!   .+�5)      �� $��Images/Excel.png��6    #��[ 6:   �� 5*      
Excel File60   5+      	 5,      _� 5-      #��[ 6"   .��6H   *(Get MS Excel file with info about events6r   5/      �� 51      -   5        #��[  51      ${�False��6R   .��52      �� $ 
Horizontal=�6!   .+�53      �� $��Images/Refresh.png��6    #��[ 6<   �� 54      Update6,   55      	 56      _� 57      #��[ 6"   .��6H   Get data form HLTV.com6`   59      �� 5;      -   5        #��[  5;      ${�False��6R   .��5<      �� $ 
Horizontal=�6!   .+�5=      �� $��Images/Clear.png��6    #��[ 6:   �� 5>      
Clear DB6.   5?      	 5@      _� 5A      #��[ 6"   .��6H   Clear all items in data base6f   5C      �� 5E      -   5        $ InfoBtn��5E      #��[  6*   .��5F      �� $ 
Horizontal=�6!   .+�5G      �� $��Images/Info.png��6    #��[ 69   �� 5H      Info6*   5I      	 5J      _� 5K      #��[ 6"   .��6H   $"Read information about application6l   5M      �� 5O      -   5        #��[  5O      .��5P      �� $ 
Horizontal=�6!   .+�5Q      �� $��Images/Exit.png��6    #��[ 69   �� 5R      Exit6*   5S      	 5T      _� 5U      #��[ 6"   .��6H   Leave the application6_   5W      5X      �� 5Z      $ Vertical=�6   $	��Top=�60   .+�5[      ��   	TextStyle#��[ 6   	Today is:6J   �� 5\      -   5        
 ��Name$
 TodayDateText��5\      #��[ 63   �� 5]      #��[ 6   Last update:6M   �� 5^      -	   5        $
 LastUpdateText��5^      #��[ 64   �� 5_      #��[ 6   You choosed:6M   �� 5`      -
   5        $
 ChoosedEventText��5`      #��[ 66   $
��Wrap=�6Y   �� 5b      -   5        $ ShowTeamsBtn��5b   ;   #��[  6   .��5c      �� $ 
Horizontal=�6!   .+�5d      �� $��Images/Team.png��6    #��[ 69   �� 5e      
Show teams60   5f      	 5g      _� 5h      #��[ 6"   .��6H   42Get information about attending teams at the event6|   5j      �� 5l      -   5        $ BackToEventsBtn��5l   ;   #��[  6   $y�	Collapsed=�6r   .��5m      �� $ 
Horizontal=�6!   .+�5n      �� $��Images/Back.png��6    #��[ 69   �� 5o      Back to events64   5p      	 5q      _� 5r      #��[ 6"   .��6H   Back to table with events6c   5t      5u      5v      �� 5x   
   -   5         ��Name$ Scroller��5y   	   $��1��5x      ���#2d3844$
��Auto=�6<   .��5z      &   System.Windows.Controls.DataGrid  -   5          Name$ 
EventsGrid��5z         AutoGenerateColumns$ False��6+     DataGridStyle#��[ 6G      HeadersVisibility$ Column=�5{         
IsReadOnly$
 True��6a      Columns 5|      0  *System.Windows.Controls.DataGridTextColumn  5}      ,  &System.Windows.Controls.DataGridColumn   Header$ Event��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 280 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   ��  �� Path$
 Name�6V     5~      $ Dates��6)   $	 150 6V    68   �� $
 Date�6V     5      $ Location��6)   $	 140 6]    6;   �� $ Location�6]     5�      $ Prize��6)   $ 75 6[    68   �� $ 	PrizePool�6[     5�      $ Amount of teams��6)   $	 100 6f    6B   �� $ 
TeamAmount�6f   5�      5�        5�   
   -   5        $ 	TeamsGrid��5�   L   $��1��5�      $y�	Collapsed=�6#   $ Column=�6:   $
 True��6V   $ False��5�   	   #��[ 6%   ���#2d3844 5�        5�      $
 Name��6%    63   �� $
 Name�  5�      $ Rating��6%    65   �� $ Rating�5�      5�      5�      