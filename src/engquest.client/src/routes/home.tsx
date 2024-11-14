import {Button} from "@nextui-org/react";
import {Link} from "react-router-dom";
import HomeImage from "../icons/home-image.tsx";

export default function Home() {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 place-items-center w-full h-full">
      <HomeImage/>
      <div className="flex flex-col items-center">
        <div className="text-2xl text-primary mb-5 text-center">
          <div>
            Добро пожаловать в <b>EngQuest</b> – ваше легендарное приключение по изучению английского языка!
          </div>
          <br/>
          <div>
            Готовьтесь, герои! <b>EngQuest</b> – это ваша конечная арена для завоевания английского языка. Отправляйтесь в эпические квесты, решайте сложные головоломки и зарабатывайте награды, повышая свои навыки. Наши интерактивные миссии и яркое сообщество делают изучение английского веселым, захватывающим и по-настоящему увлекательным.
          </div>
          <br/>
          <div>
            Присоединяйтесь к квесту сегодня и станьте чемпионом английского языка, которым вам суждено быть. Вперед, на квест! 🎮🔤🌟
          </div>
        </div>
        <Button radius="lg" className="bg-gradient-to-tr from-blue-500 to-pink-500 text-white text-2xl shadow-xl p-7">
          <Link to="/lessons" className="after:absolute after:inset-0 text-background">
            Начать приключение
          </Link>
        </Button>
      </div>
    </div>
  );
}