using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System;

namespace _2D {
  public static class NetworkClient {
    public struct NetworkData {
      public IPEndPoint IP;
      public UdpClient udpClient;
    }

    public struct SecondPlayer {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string nickname;
      public float x;
      public float y;
    }

    public struct SendData {
      public PlayerData playerData;
      public WelcomeData welcomeData;
    }

    public struct PositionData {
      public float x;
      public float y;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
      public string msg;
    }

    public struct PlayerData {
        public PositionData positionData;
        public string playerID;
    }

    public struct WelcomeData {

    }

    public unsafe struct ServerPlayerData {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
      public string playerName;
      public float x;
      public float y;
    }

    public class ServerData {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
      public string playerName;
      public float x;
      public float y;
      ServerData next;
    }

    public static Byte[] GetBytes(PositionData positionData) {
      int size = Marshal.SizeOf(positionData);
      Byte[] array = new Byte[size];
      IntPtr ptr = Marshal.AllocHGlobal(size);
      Marshal.StructureToPtr(positionData, ptr, true);
      Marshal.Copy(ptr, array, 0, size);
      Marshal.FreeHGlobal(ptr);
      return array;
    }

    public static PositionData FromBytes(Byte[] array) {
      PositionData positionData = new PositionData();

      int size = Marshal.SizeOf(positionData);
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.Copy(array, 0, ptr, size);

      positionData = (PositionData)Marshal.PtrToStructure(ptr, positionData.GetType());
      Marshal.FreeHGlobal(ptr);

      return positionData;
    }

    public static Byte[] GetBytesFromSecondPlayerStruct(SecondPlayer secondPlayer) {
      int size = Marshal.SizeOf(secondPlayer);
      Byte[] array = new Byte[size];
      IntPtr ptr = Marshal.AllocHGlobal(size);
      Marshal.StructureToPtr(secondPlayer, ptr, true);
      Marshal.Copy(ptr, array, 0, size);
      Marshal.FreeHGlobal(ptr);
      return array;
    }

    public static SecondPlayer FromBytesToSecondPlayer(Byte[] array) {
      SecondPlayer secondPlayer = new SecondPlayer();

      int size = Marshal.SizeOf(secondPlayer);
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.Copy(array, 0, ptr, size);

      secondPlayer = (SecondPlayer)Marshal.PtrToStructure(ptr, secondPlayer.GetType());
      Marshal.FreeHGlobal(ptr);

      return secondPlayer;
    }

    public static Int32[] FromBytesToInt32(Byte[] array) {
      Int32[] ints = {0,0,0,0};

      int size = Marshal.SizeOf(ints);
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.Copy(array, 0, ptr, size);

      ints = (Int32[])Marshal.PtrToStructure(ptr, ints.GetType());
      Marshal.FreeHGlobal(ptr);

      return ints;
    }

    public static ServerPlayerData ServerDataFromBytes(Byte[] array) {
      ServerPlayerData serverPlayerData = new ServerPlayerData();

      int size = Marshal.SizeOf(serverPlayerData);
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.Copy(array, 0, ptr, size);

      serverPlayerData = (ServerPlayerData)Marshal.PtrToStructure(ptr, serverPlayerData.GetType());
      Marshal.FreeHGlobal(ptr);

      return serverPlayerData;
    }

    public static ServerData FromBytesToClassReference(Byte[] array) {
      ServerData serverData = new ServerData();

      int size = Marshal.SizeOf(serverData);
      IntPtr ptr = Marshal.AllocHGlobal(size);

      Marshal.Copy(array, 0, ptr, size);

      serverData = (ServerData)Marshal.PtrToStructure(ptr, serverData.GetType());
      Marshal.FreeHGlobal(ptr);

      return serverData;
    }

    public static Byte[] GetBytes(SendData sendData) {
      int size = Marshal.SizeOf(sendData);
      Byte[] array = new Byte[size];
      IntPtr ptr = Marshal.AllocHGlobal(size);
      Marshal.StructureToPtr(sendData, ptr, true);
      Marshal.Copy(ptr, array, 0, size);
      Marshal.FreeHGlobal(ptr);
      return array;
    }
  }
}