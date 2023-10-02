# cassette-to-mp3

Work in progress to convert audio cassettes to MP3 files. There are currently separate applications for each step of the workflow. This is a work-in-progress, and these will be combined where possible.

*Note: These projects all use NAudio for recording, editing, and converting.*

## Projects
1. WAVRecorder  
Desktop application to record from a PC audio source (such as LineIn) to a WAV file.  
I use this to record a side of a cassette tape from and an Akai HX-1C through a PC input.

2. WAVSplitter  
Desktop application to split a WAV file into separate files based on provided timestamps.  
I use this to split a recording of a full side of a cassette tape into individual tracks.  
*Note: I get the timestamps by looking at the WAV file in an audio editor.*  

3. WAVtoMP3  
Desktop application to convert the separate wave files into MP3s based on provided track names.  

4. MP3Tagger
Desktop application to add ID3 tags to MP3 files.  
***Work in Progress***  

5. WAVMaker  
Console application used to test the basics of recording.  

## Enhancements
I've got the following features planned.  

* Combination of splitting and converting to MP3 as a single application.  
* Add MP3 tags (artist, album, track, year, etc.)  
* See if I can figure out a way to "auto-split" (although I'm not sure I would trust this since albums vary in moving from track to track).  

## Notes  
This started because I got a portable boom box that converted tapes to MP3. The quality was truly awful (and the MP3s were encoded strangely). This inspired me to hook up my good cassette deck to my PC to get better recordings, and possibly run them through audio editing software before converting them to MP3s.