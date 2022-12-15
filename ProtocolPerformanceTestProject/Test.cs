using System;
using MessagePack;
using FlatBuffers;
using ArangoDB.VelocyPack;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ProtoBuf;

namespace ProtocolPerformanceTest
{
    [MessagePackObject]
    [ProtoContract]
    public struct Packet
    {
        [Key(0)]
        [ProtoMember(1)]
        public Int32 header;
        [Key(1)]
        [ProtoMember(2)]
        public Int32 size;
    }

    [MessagePackObject]
    [ProtoContract]
    public struct Vector
    {
        [Key(0)]
        [ProtoMember(1)]
        public float x { get; set; }
        [Key(1)]
        [ProtoMember(2)]
        public float y { get; set; }
        [Key(2)]
        [ProtoMember(3)]
        public float z { get; set; }

    }
    [MessagePackObject]
    [ProtoContract]
    public struct Player
    {
        [Key(0)]
        [ProtoMember(1)]
        public string Id { get; set; }

        [Key(1)]
        [ProtoMember(2)]
        public string Name { get; set; }

        [Key(2)]
        [ProtoMember(3)]
        public Int32 age { get; set; }

        [Key(3)]
        [ProtoMember(4)]
        public Vector pos { get; set; }
    }

    [MessagePackObject]
    [ProtoContract]
    public class PlayerList
    {
        [Key(0)]
        [ProtoMember(1)]
        public List<Player> players { get; set; }
    }
    [MessagePackObject]
    [ProtoContract]
    public class PlayerArray
    {
        [Key(0)]
        [ProtoMember(1)]
        public Player[] players { get; set; }
    }

    public static class TestConstants
    {
        static public int maxListCount = 100000;
    }

    internal class Program
    {
       
        static void Main(string[] args)
        {

            //패킷 예시
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> Packet Test <<<<<<<<<<<<<<<<<<<<<<<");

            var pac = new Packet
            {
                header = 10131251,
                size = 1000
            };

            MessagePackTest(pac);
            VelocyPackTest(pac);
            FlatBuffersTest(pac);
            GoogleProto3Test(pac);
            ProtoBufNetTest(pac);

            /*
            //패킷[] 예시
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> Packet[] Test <<<<<<<<<<<<<<<<<<<<<<<");

            Packet[] packets = new Packet[TestConstants.maxListCount];

            for (int i = 0; i < TestConstants.maxListCount; i++) {
                packets[i] = new Packet
                {
                    header = i *2,
                    size = i * 3
                };
            }
            MessagePackTest(packets);
            VelocyPackTest(packets);
            //FlatBuffersTest(pac);
            GoogleProto3Test(packets);
            ProtoBufNetTest(packets);
            */

            //패킷List 예시
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> PacketList Test <<<<<<<<<<<<<<<<<<<<<<<");

            List<Packet> packetss = new List<Packet>();

            for (int i = 0; i < TestConstants.maxListCount; i++)
            {
                packetss.Add(new Packet
                {
                    header = i * 2,
                    size = i * 3
                });
            }
            MessagePackTest(packetss);
            VelocyPackTest(packetss);
            //FlatBuffersTest(pac);
            GoogleProto3Test(packetss);
            ProtoBufNetTest(packetss);

            /*
            //int[] 예시
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> int[] Test <<<<<<<<<<<<<<<<<<<<<<<");

            int[] ints = new int[TestConstants.maxListCount];

            for (int i = 0; i < TestConstants.maxListCount; i++)
            {
                ints[i] = i;
            }
            MessagePackTest(ints);
            VelocyPackTest(ints);
            //FlatBuffersTest(pac);
            GoogleProto3Test(ints);
            ProtoBufNetTest(ints);
            */

            // 플레이어 구조체
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> PlayerStruct Test <<<<<<<<<<<<<<<<<<<<<<<");

            var player = new Player
            {
                Id = "dkdleasdsadsaasfsafsl",
                Name = "이름",
                age = 0,
                pos = new Vector { x = 0, y = 1, z = 3 }
            };
            /*
            for (int i = 0; i < player.friends.Length; i++)
                player.friends[i] = i;
            */

            MessagePackTest(player);
            VelocyPackTest(player);
            FlatBuffersTest(player);
            GoogleProto3Test(player);
            ProtoBufNetTest(player);


            // List<Player> 데이터
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> List<Player> length = 10000 Test <<<<<<<<<<<<<<<<<<<<<<<");
            PlayerList playerList = new PlayerList();
            playerList.players = new List<Player>();

            for (int i = 0; i < TestConstants.maxListCount; i++)
            {
                playerList.players.Add(new Player
                {
                    Id = "dkdleasdsadsaasfsafsl",
                    Name = "이름",
                    age = 10,
                    pos = new Vector { x = 0, y = 1, z = 3 }
                });
            }
            MessagePackTest(playerList);
            VelocyPackTest(playerList);
            FlatBuffersTest(playerList);
            GoogleProto3Test(playerList);
            ProtoBufNetTest(playerList);

            //큰 string데이터
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> LargeStringSample.txt String Test <<<<<<<<<<<<<<<<<<<<<<<");

            string largeString = File.ReadAllText("LargeStringSample.txt");
            MessagePackTest(largeString);
            VelocyPackTest(largeString);
            FlatBuffersTest(largeString);
            GoogleProto3Test(largeString);
            ProtoBufNetTest(largeString);

            //큰 char[]데이터
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> LargeStringSample.txt Char[] Test <<<<<<<<<<<<<<<<<<<<<<<");
            char[] largeChars = File.ReadAllText("LargeStringSample.txt").ToCharArray();
            MessagePackTest(largeChars);
            VelocyPackTest(largeChars);
            //FlatBuffersTest(largeChars); 어떻게하는지 모르겠음..
            //GoogleProto3Test(largeChars); // uint16 타입이 없음
            ProtoBufNetTest(largeChars);    // uint16으로 직렬화됨

            /*
            //객체 배열 데이터
            Console.WriteLine();
            Console.WriteLine(">>>>>>>>>>>>>>>>>>>>> Player[] length = 1000000 Test <<<<<<<<<<<<<<<<<<<<<<<");
            PlayerArray playerArray = new PlayerArray();
            playerArray.players = new Player[TestConstants.maxListCount];
            for (int i = 0; i < TestConstants.maxListCount; i++)
            {
                playerArray.players[i] = new Player
                {
                    Id = "dkdleasdsadsaasfsafsl",
                    Name = "이름",
                    pos = new Vector { x = 0, y = 1, z = 3 },
                    age = 10,
                    friends = new int[2000]

                };
            }
            MessagePackTest(playerArray);
            VelocyPackTest(playerArray);
            FlatBuffersTest(playerArray);
            GoogleProto3Test(playerArray);
            ProtoBufNetTest(playerArray);
            */
        }
        static public void VelocyPackTest<T>(T data)
        {
            Console.WriteLine();
            Console.WriteLine("<<VelocyPack>> " + typeof(T));

            byte[] serializedData = VelocyPackSerialize<T>(data);
            T obj = VelocyPackDeserialize<T>(serializedData);

            PrintPlayerInfo(obj);
        }
        static public void MessagePackTest<T>(T data)
        {
            Console.WriteLine();
            Console.WriteLine("<<MessagePack>> " + typeof(T));

            byte[] serializedData = MessagePackSerialize<T>(data);
            T obj = MessagePackDeserialize<T>(serializedData);

            PrintPlayerInfo(obj);

        }
        /// <summary>
        /// flat버퍼는 미리 스키마 정의가 필요해서 T로 정의하기 쉽지않음.
        /// </summary>
        /// <param name="player"></param>
        static public void FlatBuffersTest<T>(T data)
        {
            Console.WriteLine();
            Console.WriteLine("<<FlatBuffer>> " + data.GetType());

            byte[] serializedData = FlatBuffersSerialize<T>(data);
            object obj = FlatBuffersDeserialize<T>(serializedData);

            PrintPlayerInfo(obj);
        }

        static public void GoogleProto3Test<T>(T data)
        {

            Console.WriteLine();
            Console.WriteLine("<<GoogleProto3>> " + data.GetType());

            byte[] serializedData = GoogleProto3Serialize<T>(data);
            object obj = GoogleProto3Deserialize<T>(serializedData);

            PrintPlayerInfo(obj);
        }

            static public void ProtoBufNetTest<T>(T data)
        {

            Console.WriteLine();
            Console.WriteLine("<<ProtoBuf-Net>> " + data.GetType());


            byte[] serializedData = ProtoBufNetSerialize<T>(data);
            object obj = ProtoBufNetDeserialize<T>(serializedData);

            PrintPlayerInfo(obj);
        }
        static private byte[] VelocyPackSerialize<T>(T data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            byte[] vpack = VPack.Serialize(data);
            Console.WriteLine(vpack.Length);
            sw.Stop();
            Console.WriteLine("Serialize = {0}", sw.Elapsed);

            return vpack;

        }
        static private T VelocyPackDeserialize<T>(byte[] data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            T playerResult = VPack.Deserialize<T>(data);
            sw.Stop();
            Console.WriteLine("Deserialize = {0}", sw.Elapsed);

            return playerResult;

        }
        static public byte[] MessagePackSerialize<T>(T data)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            //직렬화
            byte[] bytes = MessagePackSerializer.Serialize(data);
            Console.WriteLine(bytes.Length);

            sw.Stop();
            Console.WriteLine("Serialize = {0}", sw.Elapsed);

            return bytes;

        }
        static public T MessagePackDeserialize<T>(byte[] data)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
           
            //역직렬화
            T playerResult = MessagePackSerializer.Deserialize<T>(data);


            sw.Stop();
            Console.WriteLine("Deserialize = {0}", sw.Elapsed);

            return playerResult;
        }
        static public byte[] FlatBuffersSerialize<T>(T data)
        {

            Stopwatch sw = new Stopwatch();

            //직렬화
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            if (data is Player player)
            {
                sw.Start();

                vectorFlat.CreatevectorFlat(fbb, player.pos.x, player.pos.y, player.pos.z);
                var nameOffset = fbb.CreateString(player.Name);
                var idOffset = fbb.CreateString(player.Id);
                //VectorOffset bv = PlayerFlatBuf.CreateFriendsVector(fbb, player.friends);

                PlayerFlatBuf.StartPlayerFlatBuf(fbb);
                PlayerFlatBuf.AddName(fbb, nameOffset);
                PlayerFlatBuf.AddId(fbb, idOffset);
                PlayerFlatBuf.AddAge(fbb, player.age);
                PlayerFlatBuf.AddPos(fbb, vectorFlat.CreatevectorFlat(fbb, player.pos.x, player.pos.y, player.pos.z));
                //PlayerFlatBuf.AddFriends(fbb, bv);
                var endOffset = PlayerFlatBuf.EndPlayerFlatBuf(fbb);
                fbb.Finish(endOffset.Value);
            }
            else if (data is string str)
            {
                sw.Start();

                StringOffset off = fbb.CreateString(str);
                fbb.AddOffset(off.Value);
                fbb.StartTable(1);
                fbb.Finish(fbb.EndTable());
            }
            else if (data is PlayerList || data is PlayerArray )
            {
                sw.Start();

                Offset<PlayerFlatBuf>[] offset = new Offset<PlayerFlatBuf>[TestConstants.maxListCount];
                for (int i = 0; i < TestConstants.maxListCount; i++)
                {
                    Player tmp = new Player();
                    if (data is PlayerList pl)
                    {
                        tmp = pl.players[i];

                    } 
                    else if(data is PlayerArray pa )
                    {
                        tmp = pa.players[i];

                    }

                   // VectorOffset bv = PlayerFlatBuf.CreateFriendsVector(fbb,tmp.friends);

                    var nameOffset = fbb.CreateString(tmp.Name);
                    var idOffset = fbb.CreateString(tmp.Id);

                    PlayerFlatBuf.StartPlayerFlatBuf(fbb);

                    PlayerFlatBuf.AddName(fbb, nameOffset);
                    PlayerFlatBuf.AddId(fbb, idOffset);
                    PlayerFlatBuf.AddAge(fbb, tmp.age);
                    PlayerFlatBuf.AddPos(fbb, vectorFlat.CreatevectorFlat(fbb, tmp.pos.x, tmp.pos.y, tmp.pos.z));
                    //PlayerFlatBuf.AddFriends(fbb, bv);
                    var pEndOffset = PlayerFlatBuf.EndPlayerFlatBuf(fbb);

                    offset[i] = pEndOffset;

                }

                VectorOffset fv = PlayerListFlat.CreatePlayersVector(fbb, offset);

                PlayerListFlat.StartPlayerListFlat(fbb);
                PlayerListFlat.AddPlayers(fbb, fv);
                var EndOffset = PlayerListFlat.EndPlayerListFlat(fbb);
                fbb.Finish(EndOffset.Value);
            }
            else if(data is Packet pk)
            {
                sw.Start();
                packetFlat.StartpacketFlat(fbb);
                packetFlat.AddHeader(fbb, pk.header);
                packetFlat.AddSize(fbb, pk.size);
                fbb.Finish(packetFlat.EndpacketFlat(fbb).Value);

            }

            byte[] packet = fbb.SizedByteArray();
            Console.WriteLine(packet.Length);
            sw.Stop();

            Console.WriteLine("Seserialize = {0}", sw.Elapsed);

            return packet;
        }
        static public object FlatBuffersDeserialize<T>(byte[] data)
        {

            Stopwatch sw = new Stopwatch();
            object result = null;
            //역직렬화 
            if (typeof(T) == typeof(Player))
            {
                sw.Start();
                PlayerFlatBuf playerResult = PlayerFlatBuf.GetRootAsPlayerFlatBuf(new ByteBuffer(data));

                sw.Stop();
                result = new Player
                {
                    Id = playerResult.Id,
                    Name = playerResult.Name,
                    age = playerResult.Age,
                    pos = new Vector { x = playerResult.Pos.Value.X, y = playerResult.Pos.Value.Y, z = playerResult.Pos.Value.Z }
                    //friends = playerResult.GetFriendsArray()
                };

            }
            else if (typeof(T) == typeof(string))
            {
                sw.Start();
                result = new ByteBuffer(data).GetStringUTF8(0, data.Length - 1);

                sw.Stop();

            }
            else if (typeof(T) == typeof(PlayerList) || typeof(T) == typeof(PlayerArray))
            {
                List<Player> list = new List<Player>();
                sw.Start();
               
                result = PlayerListFlat.GetRootAsPlayerListFlat(new ByteBuffer(data));
                sw.Stop();
            }
            else if (typeof(T) == typeof(Packet))
            {
                sw.Start();
                result = packetFlat.GetRootAspacketFlat(new ByteBuffer(data));
                sw.Stop();
            }

            Console.WriteLine("Deserialize ={0}", sw.Elapsed);
            return result;
        }

        static private byte[] GoogleProto3Serialize<T>(T data)
        {
            MemoryStream stream = new MemoryStream();
            var writeStream = new Google.Protobuf.CodedOutputStream(stream, true);

            Stopwatch sw = new Stopwatch();

            if (data is Packet packet)
            {
                var target = new Proto3.Packet
                {
                    Header = packet.header,
                    Size = packet.size
                };

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    target.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is Packet[] packetArray)
            {
                var targets = new Proto3.Packets();

                foreach (Packet p in packetArray)
                {
                    var target = new Proto3.Packet
                    {
                        Header = p.header,
                        Size = p.size
                    };
                    targets.Packet.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is List<Packet> packetList)
            {
                var targets = new Proto3.Packets();

                foreach (Packet p in packetList)
                {
                    var target = new Proto3.Packet
                    {
                        Header = p.header,
                        Size = p.size
                    };
                    targets.Packet.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is Player player)
            {
                var target = new Proto3.Player
                {
                    Id = player.Id,
                    Name = player.Name,
                    Age = player.age,
                    Pos = new Proto3.Vector
                    {
                        X = player.pos.x,
                        Y = player.pos.y,
                        Z = player.pos.z
                    }
                };

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    target.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is PlayerArray playerArray)
            {
                var targets = new Proto3.Players();

                foreach (Player p in playerArray.players)
                {
                    var target = new Proto3.Player
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Age = p.age,
                        Pos = new Proto3.Vector
                        {
                            X = p.pos.x,
                            Y = p.pos.y,
                            Z = p.pos.z
                        }
                    };

                    targets.Player.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is PlayerList playerList)
            {
                var targets = new Proto3.Players();

                foreach (Player p in playerList.players)
                {
                    var target = new Proto3.Player
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Age = p.age,
                        Pos = new Proto3.Vector
                        {
                            X = p.pos.x,
                            Y = p.pos.y,
                            Z = p.pos.z
                        }
                    };

                    targets.Player.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is int[] ints)
            {
                var targets = new Proto3.Ints();

                foreach (int integer in ints)
                {
                    var target = integer;

                    targets.I.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is string str)
            {
                var target = new Proto3.String
                {
                    Str = str
                };

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    target.WriteTo(writeStream);
                }

                sw.Stop();
            }
            else if (data is char[] charArray)
            {
                var targets = new Proto3.Chars();

                foreach (char c in charArray)
                {
                    var target = new Proto3.Char
                    {
                        Char_ = c
                    };
                    targets.Chars_.Add(target);
                }

                sw.Start();

                using (writeStream = new Google.Protobuf.CodedOutputStream(stream, true))
                {
                    targets.WriteTo(writeStream);
                }

                sw.Stop();
            }

            byte[] byteData = stream.ToArray();
            Console.WriteLine(byteData.Length);

            Console.WriteLine("Serialize = {0}", sw.Elapsed);

            return byteData;
        }

        static private object GoogleProto3Deserialize<T>(byte[] data)
        {
            object result = null;
            MemoryStream stream = new MemoryStream(data);

            Stopwatch sw = new Stopwatch();

            // 역직렬화
            if (typeof(T) == typeof(Player))
            {
                result = default(Proto3.Player);

                sw.Start();
                result = Proto3.Player.Parser.ParseFrom(data);
                sw.Stop();
            }
            else if (typeof(T) == typeof(int[]))
            {
                result = default(int[]);

                sw.Start();
                result = Proto3.Ints.Parser.ParseFrom(data);
                sw.Stop();
            }
            else if (typeof(T) == typeof(string) || typeof(T) == typeof(char[]))
            {
                //result = default(string);

                sw.Start();
                result = Proto3.String.Parser.ParseFrom(data);
                sw.Stop();
            }
            else if (typeof(T) == typeof(PlayerList) || typeof(T) == typeof(PlayerArray))
            {
                sw.Start();
                result = Proto3.Players.Parser.ParseFrom(data);
                sw.Stop();
            }
            else if (typeof(T) == typeof(Packet))
            {
                sw.Start();
                result = Proto3.Packet.Parser.ParseFrom(data);
                sw.Stop();
            }
            else if (typeof(T) == typeof(Packet[]) || typeof(T) == typeof(List<Packet>))
            {
                sw.Start();
                result = Proto3.Packets.Parser.ParseFrom(data);
                sw.Stop();
            }

            Console.WriteLine("Deserialize = {0}", sw.Elapsed);

            return result;
        }

        static private byte[] ProtoBufNetSerialize<T>(T data)
        {
            MemoryStream stream = new MemoryStream();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            // 직렬화
            Serializer.Serialize<T>(stream, data);

            sw.Stop();

            byte[] byteData = stream.ToArray();
            Console.WriteLine(byteData.Length);

            Console.WriteLine("Serialize = {0}", sw.Elapsed);

            return byteData;

        }
        static private T ProtoBufNetDeserialize<T>(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            T result = Serializer.Deserialize<T>(stream);

            sw.Stop();

            Console.WriteLine("Deserialize = {0}", sw.Elapsed);
            
            return result;
            
        }
        static public void PrintPlayerInfo(Object obj)
        {
            //Console.WriteLine("obj len ={0}", obj.ToString().Length);
            /*
            if (obj.GetType() == typeof(Player))
            {
                Player player = (Player)obj;
                Console.WriteLine("player" +
                    "\n id : " + player.Id +
                    "\n Name : " + player.Name +
                    "\n pos : " + player.pos.x + " " + player.pos.y +
                    "\n friend : " + player.friends.Length + " " + player.friends[100] + " " + player.friends[999] +
                    "\n DummyValue : " + player.DummyValue);
            }
            else
            {
                Console.WriteLine("obj len ={0}", obj.ToString().Length);
            }*/

        }
    }
}