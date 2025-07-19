import toastr from "toastr";
import "toastr/build/toastr.min.css";

export const handleForm2Submit = async ({
  e,
  selectedTopic,
  executor,
  eventName,
  eventDate,
  eventDescription,
  link,
  level,
  formConducted,

  isCompetitionDirectionChecked,
  competitionDescription,
  participationResult,
  winnerDetails,

  supportTypes,
  supportTypesDescription,

  detailedInput,
  participants,
  customParticipants,
  totalParticipants,

  importantEvent,
  bestEvent,
  equalFormat,
  equalFormatDescription,

}) => {
  e.preventDefault();

  if (!selectedTopic) {
    toastr.error("Пожалуйста, выберите тему", "Ошибка");
    return;
  }




  let cleanedPeerFormat = equalFormat ? equalFormatDescription : ""; //peerFormatDescription

  let cleanedParticipants = detailedInput ? participants : []; //customParticipants


  const resultCustomCategories = Object.entries(cleanedParticipants)
    .filter(([_, count]) => Number(count) > 0)
    .map(([name, count]) => ({
      name,
      count: Number(count)
    }));

  let cleanedCustomParticipants = detailedInput ? customParticipants : []; //customParticipants


  // Проверка даты
  const dateElement = document.getElementById("event_date");
  const selectedDate = new Date(eventDate);
  const currentYear = new Date().getFullYear();

  if (selectedDate.getFullYear() !== currentYear) {
    dateElement?.classList.add("error");
    dateElement?.scrollIntoView({ behavior: "smooth", block: "center" });
    alert("Дата должна быть в пределах текущего года.");
    return;
  } else {
    dateElement?.classList.remove("error");
  }


  let hasError = false;

  const requiredFields = [
    //{ value: eventName, id: "event_name" },
    //{ value: projectName, id: "event_name" },
    { value: eventDate, id: "event_date" },
    { value: eventDescription, id: "event_description" },
    { value: link, id: "link" }
  ];


  // Проверка обязательных полей
  requiredFields.forEach((field) => {
    const element = document.getElementById(field.id);
    if (!field.value.trim()) {
      element.classList.add("error"); // Добавляем красный стиль
      hasError = true;
      element.scrollIntoView({ behavior: "smooth", block: "center" }); // Прокручиваем к первому незаполненному полю
    } else {
      element.classList.remove("error"); // Убираем красный стиль, если поле заполнено
    }
  });

  // Проверка на корректность ссылки
  const linkElement = document.getElementById("link");
  const links = link.split(',').map((l) => l.trim());

  // Проверка каждой ссылки
  const invalidLinks = links.filter((l) => !/^(https?|http):\/\/[^\s$.?#].[^\s]*$/.test(l));

  if (invalidLinks.length > 0) {
    linkElement.classList.add("error"); // Добавляем красный стиль
    linkElement.scrollIntoView({ behavior: "smooth", block: "center" });
    alert(`Некорректные ссылки: ${invalidLinks.join(', ')}`);
    hasError = true;
  }

  if (hasError) {
    alert("Пожалуйста, заполните все обязательные поля и исправьте ошибки.");
    return;
  }

  const buildSupportMap = (flags, descriptions) => {
    const result = [];
    for (const key in flags) {
      if (flags[key]) {
        let descKey;
        descKey = `${key}Description`;

        let description = descriptions[descKey];

        if (description == "" || description == undefined) {
          descKey = key;
          description = descriptions[descKey];
        }

        if (description?.trim()) {
          result.push({ key, description });
        }
      }
    }
    return result;
  };


  const selectedSupportTypes = buildSupportMap(supportTypes, supportTypesDescription);

  // проверка, нажати ли вообще галочка
  competitionDescription = isCompetitionDirectionChecked ? competitionDescription : null;
  participationResult = isCompetitionDirectionChecked ? participationResult : null;
  winnerDetails = isCompetitionDirectionChecked ? winnerDetails : null;

  // проверка на результат
  winnerDetails = participationResult === "participant" ? null : winnerDetails


  let createEventForm1Request = {
    themeCode: selectedTopic,
    actor: executor,
    name: eventName,
    date: eventDate,
    content: eventDescription,

    level: level,
    form: formConducted,
    isBestPractice: bestEvent,
    isValuable: importantEvent,
    equalToEqual: cleanedPeerFormat,


    createMediaLinkRequest: {
      content: link.split(',').map(l => l.trim())
    },

    createSupportrequest: {
      supports: selectedSupportTypes,
      supported: ""
    },

    createParticipantsRequest: {
      selectedCategories: resultCustomCategories,
      customCategories: cleanedCustomParticipants,
      total: totalParticipants
    },

    createConcourseRequest: {
      description: competitionDescription,
      result: participationResult,
      details: winnerDetails,
    }

  };


  try {
    const response = await fetch(`/api/ref/events/createform1`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(createEventForm1Request)
    });

    if (!response.ok) {
      const errorText = await response.text(); // получаем описание ошибки с сервера
      toastr.error(`Ошибка при сохранении: ${errorText}`, "Ошибка");
      return; // прерываем выполнение, чтобы не шло дальше
    }

    const data = await response.text();

    //Показать уведомление
    toastr.success("Данные успешно сохранены и добавлены в таблицу!", "Успех");
    window.setTimeout(function () {
      // Move to a new location or you can do something else
      window.location.href = "/";
    }, 3000);

  } catch (error) {

    // ОБРАБОТКА ОШИБКА *error*
    toastr.error("Произошла системная ошибка. Попробуйте позже.", "Ошибка");
  }
};