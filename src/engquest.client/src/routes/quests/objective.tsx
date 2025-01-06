import {BreadcrumbItem, Breadcrumbs, Button, ButtonGroup, Divider} from "@nextui-org/react";
import React, {useEffect, useState} from "react";
import {BackspaceIcon} from "../../icons/backspace-icon.tsx";
import {getRandomObjective, completeObjective, CompleteObjectiveResponse, GainExperienceResult, Quest} from "./quests.ts";
import {useLocation, useParams} from "react-router-dom";
import {experiencePerFirstQuest, experienceToAchiveFirstLevel, useUser} from "../../providers/user-provider.tsx";

interface Objective {
  objectiveId: number,
  questId: number,
  rusPhrase: string,
  wordGroups: WordGroup[],
}

interface WordGroup {
  words: string[],

  // added for frontend purposes
  index: number,
  disabled: boolean,
}

interface ClickHistory {
  groupIndex: number,
  wordGroupIndex: number,
}

function mapObjective(objective: any): Objective {
  if (!objective) {
    throw new Response("", {
      status: 404,
      statusText: "Not Found",
    });
  }

  const model = {
    objectiveId: objective.objectiveId,
    questId: objective.questId,
    rusPhrase: objective.rusPhrase,
    wordGroups: Array(objective.wordGroups.length),
  };

  for (let i = 0; i < objective.wordGroups.length; i++) {
    model.wordGroups[i] = {};
    model.wordGroups[i].words = objective.wordGroups[i];
    model.wordGroups[i].index = i;
    model.wordGroups[i].disabled = false;
  }

  return model;
}

const compliments = [
  "Молодец!",
  "Отлично!",
  "Хорошо!",
  "Гораздо лучше, чем я ожидал!",
  "Ты меня приятно удивил!",
  "Великолепно!",
  "Прекрасно!",
  "Ты меня очень обрадовал!",
  "Именно этого я давно ждал от тебя!",
  "Сказано здорово – просто и ясно!",
  "Ты, как всегда, точен!",
  "Очень хороший ответ!",
  "Талантливо!",
  "Ты сегодня прыгнул выше головы!",
  "Я поражен!",
  "Уже существенно лучше!",
  "Потрясающе!",
  "Замечательно!",
  "Прекрасное начало!",
  "Так держать!",
  "Ты на верном пути!",
  "Здорово!",
  "Это как раз то, что нужно!",
  "Я тобой горжусь!",
  "С каждым разом у тебя получается всё лучше!",
  "Мы с тобой не зря поработали!",
  "Я вижу, как ты стараешься!",
  "Ты растешь над собой!",
  "Ты многое сделал, я это вижу!",
  "Теперь у тебя точно все получится!",
];

function getRandomCompliment() {
  return compliments[Math.floor(Math.random() * compliments.length)];
}

let clickHistory: ClickHistory[] = [];
let objective: Objective = null!;
export default function Objective() {
  const maxShowGroupCount: number = 4;
  const emptyEngPhrase = ['Переведите предложение'];

  const {questId} = useParams();
  const [loading, setLoading] = useState(true);
  const [rusPhrase, setRusPhrase] = useState("");
  const [engPhrase, setEngPhrase] = useState<string[]>([]);
  const [shownGroups, setShownGroups] = useState<WordGroup[]>([]);
  const [isBackspaceDisabled, setIsBackspaceDisabled] = useState(true);
  const [completeObjectiveResponse, setCompleteObjectiveResponse] = useState<CompleteObjectiveResponse | null>(null);
  const [revalidateCounter, setRevalidateCounter] = useState(0);
  const {level, updateLevel} = useUser();
  const [gainExperienceResult, setGainExperienceResult] = useState<GainExperienceResult | null>(null);
  const location = useLocation();
  const {quest}: { quest: Quest } = location.state;

  useEffect(() => {
    setLoading(true);

    if (gainExperienceResult) {
      setTimeout(() => setGainExperienceResult(null), 3000);
    }

    getRandomObjective(Number(questId))
      .then(x => {
        objective = mapObjective(x);
        clickHistory = [];

        setRusPhrase(objective.rusPhrase);
        setShownGroups(objective.wordGroups.slice(0, maxShowGroupCount));
        setEngPhrase(emptyEngPhrase);
        setIsBackspaceDisabled(true);
        setLoading(false);
      });
  }, [revalidateCounter]);

  function handleButtonClick(event: React.MouseEvent<HTMLButtonElement>) {
    setCompleteObjectiveResponse(null);
    setGainExperienceResult(null);

    const button = event.target as HTMLInputElement;
    let word = button.textContent!;

    // uppercase first letter
    if (engPhrase.length === 1 && engPhrase[0] === emptyEngPhrase[0]) {
      word = `${word[0].toUpperCase()}${word.slice(1)}`;
    }

    if (word === 'i') {
      word = 'I';
    }

    const newEngPhrase = engPhrase[engPhrase.length - 1] === emptyEngPhrase[0]
      ? [word]
      : [...engPhrase, word];

    const groupIndex = Number(button.parentElement!.dataset['groupIndex']);
    const wordGroupIndex = Number(button.parentElement!.dataset['wordGroupIndex']);
    clickHistory.push({groupIndex, wordGroupIndex});

    const clickedGroupIndex = shownGroups.findIndex(group => group.index === wordGroupIndex);

    if (clickedGroupIndex < 0) {
      throw new Error('Clicked group index was not found in shown groups');
    }

    const maxShownWordGroupIndex = Math.max(...shownGroups.map(shownGroup => shownGroup.index));
    const nextWordGroup = objective!.wordGroups.find(wordGroup => wordGroup.index === maxShownWordGroupIndex + 1);
    if (nextWordGroup) {
      shownGroups[clickedGroupIndex] = nextWordGroup;
    } else {
      shownGroups[clickedGroupIndex].disabled = true;
    }

    setIsBackspaceDisabled(false);
    setShownGroups(shownGroups);
    setEngPhrase(newEngPhrase);

    if (newEngPhrase.length == objective!.wordGroups.length) {
      setLoading(true);

      completeObjective(objective.objectiveId, objective.questId, newEngPhrase.join(' '))
        .then(completeResponse => {
          setCompleteObjectiveResponse(completeResponse);

          if (completeResponse.completeObjectiveResult.success) {
            setRevalidateCounter(x => x + 1);
            if (completeResponse.level) {
              updateLevel(completeResponse.level);

              if (completeResponse.gainExperienceResult) {
                setGainExperienceResult(completeResponse.gainExperienceResult);
              }

            } else {
              if (level.experience < experienceToAchiveFirstLevel) {
                level.experience += experiencePerFirstQuest;
              }

              if (level.experience >= experienceToAchiveFirstLevel && level.value === 1) {
                level.value++;
              }

              updateLevel(level);
            }
          } else {
            clickHistory = [];
            shownGroups.forEach(x => x.disabled = false);
            setShownGroups(objective.wordGroups.slice(0, maxShowGroupCount));
            setEngPhrase(emptyEngPhrase);
            setIsBackspaceDisabled(true);
            setLoading(false);
          }
        });
    }
  }

  function handleBackspaceClick() {
    const lastClick = clickHistory.pop();

    if (lastClick) {
      shownGroups[lastClick.groupIndex] = objective!.wordGroups[lastClick.wordGroupIndex]
      shownGroups[lastClick.groupIndex].disabled = false;
    }

    engPhrase.pop();

    setShownGroups([...shownGroups]);
    setIsBackspaceDisabled(clickHistory.length <= 0);
    setEngPhrase(engPhrase.length == 0 ? emptyEngPhrase : engPhrase);
  }

  return loading ? (<></>) : (
    <>
      <Breadcrumbs itemClasses={{item: "text-lg"}} underline="hover" className="mb-3" color="primary">
        <BreadcrumbItem href="/quests">Квеcты</BreadcrumbItem>
        <BreadcrumbItem href={`/quests/${quest.id}/info`}>Квест {quest.id}: {quest.name}</BreadcrumbItem>
      </Breadcrumbs>
      <div className="flex justify-between text-lg md:text-3xl items-center">
        <div className="flex-1">
          <div className="text-primary">
            {gainExperienceResult?.newLevel
              ? (<>🎉 Поздравляем! 🎉 Ты достиг нового уровня! Продолжай в том же духе! 🌟✨</>)
              : (<>{rusPhrase}</>)}
          </div>
        </div>
        {gainExperienceResult && !gainExperienceResult.newLevel
          ? (<div className="animate-fade text-primary text-medium md:text-2xl opacity-0 select-none">
            +{gainExperienceResult.gainedExperience} ОП
          </div>)
          : (<></>)}
      </div>
      <Divider className="my-4"/>
      <div className="flex justify-between">
        {gainExperienceResult?.newLevel ? (
          <div className="text-primary text-lg md:text-3xl">
            🔓 Новый квест открыт! Прими вызов и покоряй новые вершины! 🗺️
          </div>
        ) : (<div className={`text-lg md:text-3xl ${completeObjectiveResponse?.completeObjectiveResult.success === false ? "text-danger" : "text-primary-100"}`}>{completeObjectiveResponse?.completeObjectiveResult.success === false ? completeObjectiveResponse.completeObjectiveResult.correctAnswer : engPhrase.join(' ')}</div>)}
        <Button variant="light" color="primary" className="text-xl" isDisabled={isBackspaceDisabled} onClick={handleBackspaceClick}>
          <span className="hidden md:inline">BACKSPACE</span>
          <BackspaceIcon width={32} height={32}/>
        </Button>
      </div>
      <Divider className="my-4"/>
      <div className="relative flex-grow">
        {completeObjectiveResponse?.completeObjectiveResult.success === true ? (
          <div className="flex justify-center items-center h-full text-xl md:text-5xl bg-success-200 text-success animate-fade absolute w-full opacity-0">
            {getRandomCompliment()}
          </div>
        ) : (<></>)}
        <div className="grid grid-cols-2 gap-5">
          {shownGroups.map((wordGroup, i) => (
            <ButtonGroup className={"grid grid-cols-2"} key={i} radius="none" data-word-group-index={wordGroup.index} data-group-index={i}>
              {wordGroup.words.map((word, j) => (
                <Button color="primary" variant="light" key={j} className="text-md md:text-2xl p-6" onClick={handleButtonClick} isDisabled={wordGroup.disabled}>
                  {wordGroup.disabled ? "" : word}
                </Button>
              ))}
            </ButtonGroup>
          ))}
        </div>
      </div>
    </>
  );
}