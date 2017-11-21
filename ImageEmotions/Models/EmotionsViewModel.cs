using System.Collections.Generic;
using System.Drawing;
using System.Web;
using Newtonsoft.Json.Linq;
using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using ImageEmotions.Models;

namespace ImageEmotions.ViewModels
{

    public class EmotionsViewModel
    {
        public EmotionsViewModel(string Json)
        {
            EmotionsModel model = new EmotionsModel();

            try
            {

                RootObject[] obj = JsonConvert.DeserializeObject<RootObject[]>(Json);

                //foreach (var emotion in obj)
                var emotion = obj.FirstOrDefault();

                model.Scores = new List<EmotionScore>()
                    {
                        new EmotionScore("anger", emotion.scores.anger*100),
                        new EmotionScore("contempt", emotion.scores.contempt*100),
                        new EmotionScore("happiness", emotion.scores.happiness*100),
                        new EmotionScore("surprise", emotion.scores.surprise*100),
                        new EmotionScore("disgust", emotion.scores.disgust*100),
                        new EmotionScore("fear", emotion.scores.fear*100),
                        new EmotionScore("sadness", emotion.scores.sadness*100),
                        new EmotionScore("neutral", emotion.scores.neutral*100)
                    };
                model.Face = new Rectangle(emotion.faceRectangle.left, emotion.faceRectangle.top,
                    emotion.faceRectangle.width, emotion.faceRectangle.height);

            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                Console.WriteLine(ex.Message);
            }

            this.Emotions = model;
        }

        public EmotionsModel Emotions { get; set; }

        public Image Image { get; set; }
    }

    public class EmotionsModel
    {
        public EmotionsModel()
        {
            Face = new Rectangle();
            Scores = new List<EmotionScore>();
        }

        public Rectangle Face { get; set; }
        public List<EmotionScore> Scores { get; set; }
    }

    public class EmotionScore
    {
        public EmotionScore(string Key, double Value)
        {
            this.key = Key;
            this.value = Value;
        }

        public string key { get; set; }
        public double value { get; set; }
        public string color { get; set; }
    }
}