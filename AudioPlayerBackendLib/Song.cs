﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace AudioPlayerBackendLib
{
    [DataContract]
    public struct Song : IEquatable<Song>
    {
        private const string seperator = " - ", unkownArtist = "Unkown";

        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Artist { get; private set; }

        [DataMember]
        public string FullPath { get; private set; }

        public Uri Uri { get { return new Uri(FullPath); } }

        public Song(string path)
        {
            Index = -1;

            string fileName = Path.GetFileNameWithoutExtension(path);
            int seperatorIndex = fileName.IndexOf(seperator);

            if (seperatorIndex != -1)
            {
                Artist = fileName.Remove(seperatorIndex);
                Title = fileName.Remove(0, seperatorIndex + seperator.Length);
            }
            else
            {
                Artist = unkownArtist;
                Title = fileName;
            }

            FullPath = path;
        }

        public Song(int index, string title, string artist, string fullPath) : this()
        {
            Index = index;
            Title = title;
            Artist = artist;
            FullPath = fullPath;
        }

        public override bool Equals(object obj)
        {
            return obj is Song && Equals((Song)obj);
        }

        public bool Equals(Song other)
        {
            return Index == other.Index &&
                   Title == other.Title &&
                   Artist == other.Artist &&
                   FullPath == other.FullPath;
        }

        public override int GetHashCode()
        {
            var hashCode = -982487627;
            hashCode = hashCode * -1521134295 + Index.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Artist);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullPath);
            return hashCode;
        }

        public static bool operator ==(Song song1, Song song2)
        {
            return song1.Equals(song2);
        }

        public static bool operator !=(Song song1, Song song2)
        {
            return !(song1 == song2);
        }
    }
}