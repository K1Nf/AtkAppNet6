import toastr from "toastr";
import "toastr/build/toastr.min.css";

export const handleForm11Submit = async ({
  e,
  selectedTopic,
  executor,
  eventName,
  eventDate,
  eventDescription,
  link,

  hasFinancing,
  financing,
  financingOtherDescription,
  isCooperation,
  selectedOrganizations,
  otherOrganizations,

  audience,
  otherAudienceDescription,

  sourcesSourceOfDistribution,
  linksSourceOfDistribution,
  otherSourceOfDistributionDescription,

}) => {
  e.preventDefault();

  if (!selectedTopic) {
    toastr.error("Пожалуйста, выберите тему", "Ошибка");
    return;
  }


  //Обнуление переменных
  let cleanedFinancing = hasFinancing ? financing : {
    municipal: "",
    regional: "",
    grants: "",
    other: ""
  };
  let cleanedFinancingOther = hasFinancing ? financingOtherDescription : "";

  let cleanedOrganizations = isCooperation ? selectedOrganizations : {};
  let cleanedCustomOrganizations = isCooperation ? otherOrganizations : [];


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


  e.preventDefault();

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

  // обработка источников распространения
  // Преобразование ключей из sources в нужный формат
  const resultSources = Object.entries(sourcesSourceOfDistribution)
    .filter(([key, value]) => value === true)
    .map(([key]) => {
      const name = key === "other" ? otherSourceOfDistributionDescription : key;

      // Формируем имя ссылки: делаем первую букву маленькой и добавляем "Link" в конец
      const linkKey = key + "Link";

      return {
        name,
        link: linksSourceOfDistribution[linkKey] || ""
      };
    });


  // обработка аудитории 
  let otherAudienceDesc = audience.other ? otherAudienceDescription : null;

  const resultAudiences = Object.entries(audience)
    .filter(([key, value]) => value === true)
    .map(([key]) => {
      if (key === "other") {
        return otherAudienceDesc;
      }
      return key;
    });



  let createEventForm1Request = {
    themeCode: selectedTopic,
    actor: executor,
    name: eventName,
    date: eventDate,
    content: eventDescription,

    createMediaLinkRequest: {
      content: link.split(',').map(l => l.trim())
    },

    createFinanceRequest: {
      MunicipalBudget: Number(cleanedFinancing?.municipal),
      RegionalBudget: Number(cleanedFinancing?.regional),
      GranteBudget: Number(cleanedFinancing?.grants),
      OtherBudget: Number(cleanedFinancing?.other),
      description: cleanedFinancingOther
    },

    createInterAgencyCooperationRequest: {
      selectedOrganizations: cleanedOrganizations,
      customOrganizations: cleanedCustomOrganizations
    },

    createSourcesOfDistributionRequest: {
      links: resultSources
    },

    createAudienceRequest: {
      audiences: resultAudiences
    }

  };


  try {
    const response = await fetch(`/api/ref/events/createform1`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        'RequestVerificationToken': localStorage.getItem('RequestVerificationToken')
      },
      body: JSON.stringify(createEventForm1Request),
      credentials: "include" // обязательно, чтобы отправилась кука XSRF-TOKEN
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