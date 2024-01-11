using System.ComponentModel.Design.Serialization;
using Microsoft.VisualBasic;

namespace Day13;

public class Packet
{
    public Message FullMessage;

    public Packet(string line)
    {
        var newline=line.Substring(1, line.Length - 2);
        FullMessage = new Message(true);
        var split = newline.Split(",");
        Message messageRead;
        Message currentMessage=FullMessage;
        for (var index = 0; index < split.Length; index++)
        {
            var segment = split[index];
            if (segment.Length == 1)
            {
                currentMessage.AddMessage(false,Int32.Parse(segment));
            }
            else if(segment.Length>1)
            {
                string intValue = "";
                bool added = false;
                foreach (var cara in segment)
                {
                    if (cara == '[')
                    {
                        messageRead = new Message(currentMessage,true);
                        currentMessage.AddMessage(messageRead);
                        currentMessage = messageRead;
                    }
                    else if (cara == ']')
                    {
                        if (intValue != "")
                        {
                            currentMessage.AddMessage(false,Int32.Parse(intValue));
                        }
                        added = true;
                        currentMessage = currentMessage.parent;
                    }
                    else
                    {
                        intValue += cara;
                    }
                }

                if (!added)
                {
                    currentMessage.AddMessage(false,Int32.Parse(intValue));
                }
            }
        }
    }

    public override string ToString()
    {
        return FullMessage.ToString();
    }

    public bool IsMessage(Packet other)
    {
        return FullMessage == other.FullMessage;
    }

    public bool Inferior(Packet other)
    {
        var response = FullMessage.Inferior(other.FullMessage);
        if (response == Message.result.NotFound)
        {
            throw new Exception(("same"));
        }
        return response==Message.result.True;
    }

    public class Message
    {
        public Message parent;
        public bool isList;
        public int value;
        public List<Message> messages;

        public Message(bool isList,int value)
        {
            this.isList = isList;
            messages = new List<Message>();
            if (!this.isList)
            {
                this.value = value;
            }
            else
            {
                messages.Add(new Message(false,value));
            }

            parent = this;
        }
        
        public Message(bool isList)
        {
            this.isList = isList;
            messages = new List<Message>();
            if (!this.isList)
            {
                this.value = 0;
            }

            parent = this;
        }
        
        public Message(bool isList,Message inside)
        {
            this.isList = isList;
            messages = new List<Message>();
            if (!this.isList)
            {
                this.value = value;
            }
            else
            {
                messages.Add(inside);
            }

            parent = this;
        }
        
        public Message(Message parent,bool isList,int value=0)
        {
            this.isList = isList;
            messages = new List<Message>();
            if (!this.isList)
            {
                this.value = value;
            }

            this.parent = parent;
        }

        public void AddMessage(Message message)
        {
            messages.Add(message);
        }
        
        public void AddMessage(bool isList,int value=0)
        {
            messages.Add(new Message(this,isList,value));
        }

        public override string ToString()
        {
            if (isList)
            {
                return "["+String.Join(",",messages)+"]";
            }

            return value.ToString();
        }

        public enum result
        {
            True,
            False,
            NotFound,
        }

        public result Inferior(Message other)
        {
            if (!isList && !other.isList)
            {
                //Console.WriteLine("Compare values "+value+" vs "+other.value);
                if (value == other.value)
                {
                    return result.NotFound;
                }
                else if(value < other.value)
                    return result.True ;
                else
                {
                    return result.False;
                }
            }
            else if (isList && other.isList)
            {
                //Console.WriteLine("Compare list "+String.Join(",",messages)+" vs "+String.Join(",",other.messages));
                int maxCount = Math.Min(messages.Count, other.messages.Count);
                var index = 0;
                for (; index < maxCount; index++)
                {
                    var message = messages[index];
                    var otherMessage = other.messages[index];
                    var resultMatch = message.Inferior(otherMessage);
                    if (resultMatch!=result.NotFound)
                    {
                        return resultMatch;
                    }
                }

                if (other.messages.Count < messages.Count)
                {
                    return result.False;
                }

                if (other.messages.Count > messages.Count)
                {
                    return result.True;
                }
                
                return result.NotFound;
                

            }
            else if (isList && !other.isList)
            {
                //Console.WriteLine("Compare list "+String.Join(",",messages)+" vs value "+other.value);
                return Inferior(new Message(true, other));
            }
            else if(!isList && other.isList)
            {
                //Console.WriteLine("Compare value "+value+" vs list "+String.Join(",",other.messages));
                var listMessage = new Message(true, this);
                return listMessage.Inferior(other);
            }
            else
            {
                throw new Exception("not int or list");
            }
        }
    }
}