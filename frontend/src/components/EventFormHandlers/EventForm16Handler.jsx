import toastr from "toastr";
import "toastr/build/toastr.min.css";

export const handleForm16Submit = async ({
  e,
  selectedTopic,
  eventDescription,
  executor,
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
    { value: eventDescription, id: "executor_description" },
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

  if (hasError) {
    alert("Пожалуйста, заполните все обязательные поля и исправьте ошибки.");
    return;
  }



  let createEventBaseRequest = {
    themeCode: selectedTopic,
    actor: executor,
    content: eventDescription,
  };


  try {
    const response = await fetch(`/api/ref/events/createbase`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        'RequestVerificationToken': localStorage.getItem('RequestVerificationToken')
      },
      body: JSON.stringify(createEventBaseRequest),
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