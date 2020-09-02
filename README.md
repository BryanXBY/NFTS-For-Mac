# NFTS-For-Mac
Enable MacOS support for NFTS disks,启用Mac系统对NFTS的支持
MCNFTS.zip是编译好的可执行文件，需要的直接下载即可。

下载MCNFTS.zip 然后放在OSX的下载目录，解压到下载目录下。

（解压完的完整目录地址为：/Users/你的账户名/Downloads/MCNFTS ）

然后打开终端输入粘贴以下命令，输入用户密码执行即可：

cd Downloads

cd MCNFTS

sudo chmod -x list.sh

sudo chmod 777 list.sh

sudo chmod -x MCNFTS

sudo chmod 777 MCNFTS

copy ./lish.sh /Users/$USER/lish.sh

cd /Users/$USER/

sudo chmod -x list.sh

sudo chmod 777 list.sh

cd Downloads

cd MCNFTS

sudo ./MCNFTS

注：因为第二次运行程序，启动目录会被自动修正为 /Users/$USER 所以会copy一份过去。
