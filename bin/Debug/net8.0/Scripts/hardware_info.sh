#!/bin/bash

# CPU Bilgileri
cpu_info=$(sysctl -n machdep.cpu.brand_string)

# RAM Bilgileri
ram_info=$(sysctl hw.memsize)

# Disk Bilgileri
disk_info=$(df -h)

# Ağ Adaptörü Bilgileri
network_info=$(ifconfig)

# İşletim Sistemi Bilgileri
os_info=$(sw_vers)

# Ekran Kartı Bilgileri (GPU)
gpu_info=$(system_profiler SPDisplaysDataType)

# Çıktıyı ekrana yazdırma
echo ""
echo "#########################################"
echo "###            CPU Bilgileri          ###"
echo "#########################################"
echo "$cpu_info"

echo ""
echo "#########################################"
echo "###            RAM Bilgileri          ###"
echo "#########################################"
echo "RAM Size: $(echo "$ram_info" | awk '{print $2 / 1073741824 " GB"}')"

echo ""
echo "#########################################"
echo "###           Disk Bilgileri          ###"
echo "#########################################"
echo "$disk_info"

echo ""
echo "#########################################"
echo "###        Ağ Adaptörü Bilgileri      ###"
echo "#########################################"
echo "$network_info"

echo ""
echo "#########################################"
echo "###      İşletim Sistemi Bilgileri    ###"
echo "#########################################"
echo "$os_info"

echo ""
echo "#########################################"
echo "###       Ekran Kartı Bilgileri       ###"
echo "#########################################"
echo "$gpu_info"
