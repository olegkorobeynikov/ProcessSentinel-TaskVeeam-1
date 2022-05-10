# ProcessSentinel-TaskVeeam-1

## ENG

Write a C# utility which monitors Windows processes and "kills" those processes, that take too long.
- The input consists of three parameters: the name of the process, the allowed lifetime (in minutes) and the frequency of checking (in minutes).
- The utility is started from the command line. On startup it reads the three input parameters and starts monitoring the processes with a specified frequency. If a process is alive for too long it shuts it down and generates a message in the log. If no processes are present the tool continues monitoring (processes may appear). The utility exits by pressing a special key (e.g. q).

Example launches: monitor.exe notepad 5 1
With these options the tool checks once per minute if the process notepad is alive for more than 5 minutes, and &quot;kills&quot; it if it is.

## RUS

Написать на C# утилиту, которая мониторит процессы Windows и &quot;убивает&quot; те процессы, которые работают слишком долго.
- На входе три параметра: название процесса, допустимое время жизни (в минутах) и частота проверки (в минутах).
- Утилита запускается из командной строки. При старте она считывает три входных параметра и начинает мониторить процессы с указанной частотой. Если процесс живет слишком долго – завершает его и выдает сообщение в лог. При отсутствии указанных процессов утилита продолжает мониторинг (процессы могут появиться). Утилита завершает работу при нажатии специальной клавиши (например, q).

Пример запуска: monitor.exe notepad 5 1
С такими параметрами утилита раз в минуту проверяет, не живет ли процесс notepad больше пяти минут, и &quot;убивает&quot; его, если живет.
