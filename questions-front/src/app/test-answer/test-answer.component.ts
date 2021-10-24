import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Question } from '../interfaces/question';
import { ResultElement } from '../interfaces/resultElement';
import { QuestionService } from '../services/question.service';

@Component({
  selector: 'app-test-answer',
  templateUrl: './test-answer.component.html',
  styleUrls: ['./test-answer.component.sass']
})
export class TestAnswerComponent implements OnInit {

  questions: Question[] = [];
  results: ResultElement[] = [];
  questionNumber = 0;

  answerForm = new FormGroup({
    answer: new FormControl(null, Validators.required)
  });

  showResults = false;

  constructor(
    private questionService: QuestionService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.questionService.getList().subscribe(data => {
      this.questions = data[this.questionService.quetionsToAnswerId];
      if(!this.questions)
        this.router.navigate(['']);
    });
  }

  nextQuestion() {
    if(!this.answerForm.valid || this.questionNumber === this.questions.length) {
      this.answerForm.markAllAsTouched();
      return;
    }
    let currentQuestion = this.questions[this.questionNumber];
    let correctAnswer = currentQuestion.answers.find((a: any) => a.id === currentQuestion.correctAnswerId)!['textAnswer'];
    this.results.push({
      question: this.questions[this.questionNumber].questionText,
      userAnswer: this.answerForm.get('answer')?.value, 
      correctAnswer: correctAnswer,
      isAnswerCorrect: correctAnswer === this.answerForm.get('answer')?.value ? true : false});
    this.questionNumber++;
    this.answerForm.reset();
    
    if(this.questionNumber === this.questions.length)
    {
      this.showResults = true;
      let trueRes = this.results.map(r => r.isAnswerCorrect).filter(r => r).length;
      let remInd = this.questionService.questionsResults.findIndex(q => q.questionIndex === this.questionService.quetionsToAnswerId);
      if(remInd != -1)
        this.questionService.questionsResults.splice(remInd, 1);
        
      this.questionService.questionsResults.push({
        questionIndex: this.questionService.quetionsToAnswerId,
        result: `${trueRes} / ${this.questions.length}`
      });
    }
  }

}
