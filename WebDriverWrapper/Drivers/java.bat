set JAVA_HOME="C:\Program Files\Java\jre7"
set JAVA="%JAVA_HOME%\bin\java.exe"
set JAVA_OPTS="%JAVA_TOOL_OPTONS% %_JAVA_OPTIONS%"
set JAVA_TOOL_OPTIONS=
set _JAVA_OPTIONS=
"%JAVA%" %JAVA_OPTS% -XX:+PrintVMOptions %*