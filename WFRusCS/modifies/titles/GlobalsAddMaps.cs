using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace WFRus.modifies.titles;

public class GlobalsAddMaps : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Singletons/globals.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "loot_tables"},
            t => t.Type is TokenType.Newline
        ], allowPartialMatch: true);

        // loop through all tokens in the script
        foreach (var token in tokens) {
            if (waiter.Check(token)) {
                // og next line
                yield return new Token(TokenType.Newline);

                Dictionary<string, string> titleNameDict = new Dictionary<string, string> {
                    ["Scout"] = "Скаут",
                    ["Second Class Scout"] = "Второклассный скаут",
                    ["First Class Scout"] = "Первоклассный скаут",
                    ["Star Scout"] = "Звёздный скаут",
                    ["Life Scout"] = "Скаут по жизни",
                    ["Eagle Scout"] = "Старший скаут",
                    ["Survival Expert"] = "Эксперт по выживанию",
                    ["Pack Leader"] = "Лидер стаи",
                    ["Headmaster"] = "Директор",
                    ["Tenderfoot"] = "Новичок",
                    ["Voyager"] = "Путешественник",
                    ["Ace"] = "Ас",
                    ["Admiral"] = "Адмирал",
                    ["Ancient"] = "Древний",
                    ["Bi"] = "Би",
                    ["Bipedal Animal Drawer"] = "Двуногая тумбочка-животное",
                    ["cadaver dog"] = "собака-кадавр",
                    ["NiceCandy"] = "NiceCandy",
                    ["Catfisher"] = "Ловец сомов",
                    ["Cozy"] = "Комфортный",
                    ["Creature"] = "Существо",
                    ["Critter"] = "Тварь",
                    ["Cryptid"] = "Криптоид",
                    ["Dude"] = "Чувак",
                    ["Elite"] = "Элита",
                    [":3"] = ":3",
                    ["Fish-Pilled"] = "Рыбинист",
                    ["Freaky"] = "Фрик",
                    ["Gay"] = "Гей",
                    ["The Title Only For People Who Caught The Super Duper Rare Golden Bass"] = "Звание только для тех, кто поймал супер пупер редкого золотого окуня",
                    ["The Title Only For People Who Caught The Super Duper Rare Golden Ray"] = "Звание только для тех, кто поймал супер пупер редкого золотого ската",
                    ["Goober"] = "Дурашка",
                    ["Good Boy"] = "Хороший мальчик",
                    ["Good Girl"] = "Хорошая девочка",
                    ["77 | 3328 FISSILE"] = "77 | 3328 FISSILE",
                    ["Igloo or Hot Dog"] = "Иглу или хот-дог",
                    ["Normal and Regular"] = "Нормальный и обычный",
                    ["Is Cool"] = "Очень крутой",
                    ["King"] = "Король",
                    ["Kitten"] = "Котик",
                    ["Koi Boy"] = "Кой-бой",
                    ["fake lamedev"] = "фейк lamedev",
                    ["lamedev"] = "lamedev",
                    ["Lesbian"] = "Лесбиянка",
                    ["Little Lad"] = "Микрочел",
                    ["Majestic"] = "Царь во дворца",
                    ["Musky"] = "Мускусный",
                    ["Night Crawler"] = "Стрингер",
                    ["nitrous oxide"] = "закусь азотом",
                    ["Non-Binary"] = "Небинарный",
                    ["Pan"] = "Кастрюля",
                    ["Pretty"] = "Милый",
                    ["Problematic"] = "Проблемный",
                    ["Pup"] = "Щенок",
                    ["Puppy"] = "Пёсик",
                    ["Queer"] = "Квир",
                    ["Shark Bait"] = "Наживка для акул",
                    ["shithead"] = "ублюдок",
                    ["Silly Guy"] = "Дурачок",
                    ["Soggy Doggy"] = "До нитки",
                    ["Special"] = "Особый",
                    ["Webfishing Special Forces"] = "Спецотряд интернет-рыбалки",
                    ["Stinker Dinker"] = "Вонючка",
                    ["\"straight\""] = "\"натурал\"",
                    ["Strongest Warrior"] = "Сильнейший воин",
                    ["Stupid Idiot Baby"] = "Тупой ребёнок-идиот",
                    ["Trans"] = "Транс",
                    ["Yapper"] = "Болтун",
                    ["ZedDog"] = "ZedDog"
                };
                var titleNameTokens = parseDictionary(titleNameDict);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("titlenamemap");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                foreach (var tkn in titleNameTokens) yield return tkn;
                yield return new Token(TokenType.CurlyBracketClose);
                
                // next line
                yield return new Token(TokenType.Newline);

                Dictionary<string, string> titleDescDict = new Dictionary<string, string> {
                    ["Welcome to Camp!"] = "Добро пожаловать в лагерь!",
                    ["Getting the ropes!"] = "Уже понимаешь, как всё завязано!",
                    ["Making a name for yourself!"] = "Зарабатываешь репутацию!",
                    ["Shining!"] = "Блестишь!",
                    ["Climbing the ladder!"] = "Взбираешься по лестнице!",
                    ["The best of the best!"] = "Лучший из лучших!",
                    ["You know it all!"] = "Всё знаешь!",
                    ["Lead by example!"] = "Веди примером!",
                    ["Keeping everyone else in line!"] = "Держишь всех в строю!",
                    ["Getting around!"] = "Бегаешь туда-сюда!",
                    ["Top of the world."] = "Вершина мира.",
                    ["Ace!"] = "Ас!",
                    ["admirable"] = "восхитительно",
                    ["Eons old!"] = "Стар, как мир!",
                    ["Bi!"] = "Би!",
                    ["RAHHHH"] = "РАХХХХ",
                    ["worlds smartest mutt"] = "умнейшая дворняга",
                    ["the nicests candy"] = "the nicests candy",
                    ["kinda like a cat but if it was fishing"] = "типа ловец снов но ловец сомов",
                    ["Nice and warm!"] = "Уютно и тепло.",
                    ["Raaaahh!"] = "Раааахх!",
                    ["just scampering around, you"] = "тупо слоняешься без дела, ты",
                    ["Mysterious!"] = "Загадочно!",
                    ["Duuuuude..."] = "Чуваааак...",
                    ["Intense!"] = "Мощно!",
                    [":3"] = ":3",
                    ["based and fish-pilled"] = "базированный рыбинист",
                    ["what if webfishing was freaky"] = "вот бы была иге по интернет рыбалке",
                    ["Gay!"] = "Гей!",
                    ["Woah!"] = "Воу!",
                    ["Goob!"] = "Гуп!",
                    ["Who'sa good boy!"] = "Кто хороший мальчик!",
                    ["Who'sa good girl!"] = "Кто хорошая девочка!",
                    ["Hazardous..."] = "Радиокартивно...",
                    ["wad of meat"] = "пачка мяса",
                    ["we know, zach."] = "мы в курсе, зак.",
                    ["The coolest."] = "Крутейший.",
                    ["Royalty!"] = "Царственно!",
                    ["Mew!"] = "Мяу!",
                    ["koiboi"] = "коибои",
                    ["due to a bug, this exists now."] = "произошёл какой-то баг, и появилось это звание",
                    ["if youre not west and you see this. you're evil."] = "если вы не уэст и видите это. вы злодей.",
                    ["Lesbian!"] = "Лесбиянка!",
                    ["Lil guy!"] = "Мини-челик!",
                    ["Graceful!"] = "Грациозно!",
                    ["Smelly!"] = "Вонь какая!",
                    ["Wormy!"] = "Джилленхол?",
                    ["very funny"] = "очень смешно",
                    ["Non-Binary!"] = "Небинарный!",
                    ["Pan!"] = "Кастрюля!",
                    ["The prettiest!"] = "Милейше!",
                    ["Yikes, dude."] = "Мда, чел.",
                    ["Yeah."] = "Ага.",
                    ["Pup!"] = "Щенок!",
                    ["How peculiar."] = "Как интересно.",
                    ["Shark Bait!"] = "Наживка для акул!",
                    ["Pronounced, Shitheed."] = "Кто, интересно, выберет такое звание?",
                    ["just a silly lil guy,,,"] = "всего лишь маленький дурашка",
                    ["Sloppy!"] = "Подскользнёшься!",
                    ["Very special :)"] = "Очень особый :)",
                    ["The W.S.F."] = "С.И.Р.",
                    ["The stinkiest!"] = "Максимальный вонючка!",
                    ["mhm"] = "угу",
                    ["The mightiest!"] = "Величайший!",
                    ["we get it box."] = "это же для друзей разраба, да?",
                    ["Trans!"] = "Транс!",
                    ["Yap yap yap!"] = "Бла бла бла!",
                    ["zeddy doggy"] = "zeddy doggy"
                };
                var titleDescTokens = parseDictionary(titleDescDict);

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("titledescmap");
                yield return new Token(TokenType.OpAssign);
                yield return new Token(TokenType.CurlyBracketOpen);
                foreach (var tkn in titleDescTokens) yield return tkn;
                yield return new Token(TokenType.CurlyBracketClose);

                // next line
                yield return new Token(TokenType.Newline);
            } else {
                // return the original token
                yield return token;
            }
        }
    }

    private IEnumerable<Token> parseDictionary(Dictionary<string, string> dict) {
        IEnumerable<Token> tkns = [];
        
        var enmr = dict.GetEnumerator();
        bool hasNext = enmr.MoveNext();

        while (hasNext) {
            var (key, val) = enmr.Current;
            hasNext = enmr.MoveNext();
                    
            tkns = tkns.Append(new ConstantToken(new StringVariant(key)));
            tkns = tkns.Append(new Token(TokenType.Colon));
            tkns = tkns.Append(new ConstantToken(new StringVariant(val)));
            if (hasNext) tkns = tkns.Append(new Token(TokenType.Comma));
        }

        return tkns;
    }
}