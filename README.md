#Protocol
```C#
struct Header 
{
    public short comm;
    public short code;
    public int size;
    public int reserved;
}

struct Packet 
{
    public Header header;
    public byte[] body;
}
```

##Header (visualized)
```
+--------+--------+--------+--------+
|    Comm Type    |       Code      |
+--------+--------+--------+--------+
|             Body Size             |
+--------+--------+--------+--------+
|             Reserved              |
+--------+--------+--------+--------+
```

##Comm Type (communication type)  
0 : client to server  
1 : server to server  

##Code  
000 : Fail  
100 : Success  
200 : Message  
-250 : Server Message  
300 : Auth  
-301 : Signup  
-302 : Login  
400 : List  
-450 : Server List  
500 : Join  
-550 : Server Join  
600 : Leave  
700 : Create  
800 : Heartbeat  
900 : Destroy  
-950 : Server Destroy  
