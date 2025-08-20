import toastr from "toastr";
import "toastr/build/toastr.min.css";

export const handleForm4Submit = async ({
  e,
  selectedTopic,
  eventDate,
  eventDescription,
  eventName,
  executor,
  supportTypes,
  supportTypesDescription,
  helpTypes,
  helpTypesDescription,
  link,
  formType
}) => {

  e.preventDefault();

  if (!selectedTopic) {
    toastr.error("Пожалуйста, выберите тему", "Ошибка");
    return;
  }

  let hasError = false;

  const requiredFields = [
    //{ value: eventName, id: "event_name" },
    //{ value: projectName, id: "event_name" },
    //{ value: eventDescription, id: "executor_description" },
  ];

  if (formType == "3.2.2" && formType === "2.7.1") {
    requiredFields.push({ value: link, id: "link" });
  }



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


  if (formType == "3.2.2" && formType === "2.7.1") {
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

        let description = descriptions[descKey]; // вызывается ошибка

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
  const selectedHelpTypes = buildSupportMap(helpTypes, helpTypesDescription);

  const combinedSupport = [
    ...selectedSupportTypes,
    ...selectedHelpTypes
  ];


  let createEventForm1Request = {
    themeCode: selectedTopic,
    actor: executor,
    content: eventDescription,
    date: eventDate,
    name: eventName,

    createSupportrequest: {
      supports: combinedSupport,
      supported: ""
    },

    createMediaLinkRequest: {
      content: link.split(',').map(l => l.trim())
    },
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